using fitnessweb.Core.Commands;
using fitnessweb.Core.Helpers;
using fitnessweb.Domain.Entities;
using fitnessweb.Domain.Types;
using fitnessweb.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace fitnessweb.Core.Handlers.Workout;

public class GenerateWorkoutCommandHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GenerateWorkoutCommand, Unit>
{
    public async Task<Unit> Handle(GenerateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var validExercises = await fitnessDbContext.Exercises
            .Include(e => e.Muscles).ThenInclude(m => m.MuscleGroup)
            .Where(e => request.Equipment.Contains(e.Equipment) &&
                        e.Difficulty <= request.Difficulty &&
                        e.Muscles.Any(m => request.MuscleGroups.Contains(m.MuscleGroupId)))
            .ToListAsync(cancellationToken);

        
        if (request.Equipment.Any(reqEq => !validExercises.Any(e => e.Equipment == reqEq)))
        {
            throw new Exception("No exercises match your selection. \n Please select additional muscle groups.");
        }

        if (!Enum.IsDefined(typeof(Goal), (Goal)request.Goal) ||
            !Enum.IsDefined(typeof(FitnessLevel), (FitnessLevel)request.Difficulty) ||
            request.Equipment.Any(e => !Enum.IsDefined(typeof(Equipment), e)))
        {
            throw new Exception("Bad enum value.");
        }
        
        if (!await fitnessDbContext.Users.AnyAsync(u => u.Id == request.UserId,cancellationToken))
        {
            throw new Exception("User doesn't exist.");
        }
        
        
        var goalParams = WorkoutGoalParameters.Get(request.Goal, request.TargetDurationMinutes);

        int maxCycles = 2 * validExercises.Count();
        
        var selectedExercises = new List<WorkoutExercise>();
        var totalSeconds = 0;
        var usedEquipment = new List<Equipment>();
        var coveredMuscleGroups = new List<Guid>();
        var random = new Random();

        var shuffledExercises = validExercises
            .OrderBy(_ => random.Next())
            .GroupBy(e => e.Difficulty)
            .OrderByDescending(g => (int)g.Key)
            .SelectMany(g => g)
            .ToList();
        
        var exerciseQueue = new Queue<Domain.Entities.Exercise>(shuffledExercises);
        var usedExerciseIds = new List<Guid>();
        int i = 0;

        while (exerciseQueue.Count > 0 && totalSeconds < request.TargetDurationMinutes * 60 && i <= maxCycles)
        {
            i++;
            var exercise = exerciseQueue.Dequeue();
            if (usedExerciseIds.Contains(exercise.Id))
                continue;
            
            var addsNewEquipment = !usedEquipment.Contains(exercise.Equipment);
            var exerciseMuscleGroupIds = exercise.Muscles.Select(m => m.MuscleGroupId).Distinct().ToList();
            var addsNewMuscleGroup = exerciseMuscleGroupIds.Any(id => request.MuscleGroups.Contains(id) && !coveredMuscleGroups.Contains(id));

            bool stillMissingRequirements =
                usedEquipment.Count < request.Equipment.Count ||
                coveredMuscleGroups.Count < request.MuscleGroups.Count;
            
            if (stillMissingRequirements && !(addsNewEquipment || addsNewMuscleGroup))
            {
                exerciseQueue.Enqueue(exercise);
                continue;
            }

            var sets = random.Next(goalParams.Sets.Item1, goalParams.Sets.Item2 + 1);
            var reps = random.Next(goalParams.RepsPerSet.Item1, goalParams.RepsPerSet.Item2 + 1);

            var timePerSet = exercise.SecondsPerSet;
            var totalTimeForExercise =
                (sets * reps * timePerSet) +
                ((sets - 1) * goalParams.RestBetweenSetsSeconds) +
                goalParams.RestBetweenExercisesSeconds;

            if (totalSeconds + totalTimeForExercise > request.TargetDurationMinutes * 60)
                continue;

            selectedExercises.Add(new WorkoutExercise
            {
                ExerciseId = exercise.Id,
                Exercise = exercise,
                Sets = sets,
                RepsPerSet = reps
            });

            usedExerciseIds.Add(exercise.Id);
            if (!usedEquipment.Contains(exercise.Equipment))
            {
                usedEquipment.Add(exercise.Equipment);
            }

            foreach (var groupId in exerciseMuscleGroupIds)
            {
                if (request.MuscleGroups.Contains(groupId) && !coveredMuscleGroups.Contains(groupId))
                    coveredMuscleGroups.Add(groupId);
            }

            totalSeconds += totalTimeForExercise;
            Console.WriteLine($"{i} Current e: {usedEquipment.Count} target equipment {request.Equipment.Count}");
            Console.WriteLine($"{i} Current e: {coveredMuscleGroups.Count} target muscles {request.MuscleGroups.Count}");
        }

        if (usedEquipment.Count < request.Equipment.Count || coveredMuscleGroups.Count < request.MuscleGroups.Count)
        {
            throw new Exception("The workout time is not long enough to generate workout.");
        }
        
        var muscleGroups = await fitnessDbContext.MuscleGroups
            .Where(mg => request.MuscleGroups.Contains(mg.Id))
            .ToListAsync(cancellationToken);
        
        var workout = new Domain.Entities.Workout
        {
            UserId = request.UserId,
            Name = request.Name,
            Difficulty = request.Difficulty,
            TargetDurationMinutes = (int)Math.Round(totalSeconds / 60.0),
            Goal = request.Goal,
            MuscleGroups = muscleGroups,
            Equipment = usedEquipment,
            WorkoutExercises = selectedExercises,
        };

        await fitnessDbContext.Workouts.AddAsync(workout, cancellationToken);
        await fitnessDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}