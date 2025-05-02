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
                        e.Difficulty <= request.Difficulty)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        var requiredMuscleGroups = await fitnessDbContext.MuscleGroups
            .Where(mg => request.MuscleGroups.Contains(mg.Id))
            .Include(mg => mg.Muscles)
            .ToListAsync(cancellationToken);
        
        var goalParams = WorkoutGoalParameters.Get(request.Goal);
        
        var selectedExercises = new List<WorkoutExercise>();
        var totalSeconds = 0;
        var usedEquipment = new HashSet<Equipment>();
        var coveredMuscleGroups = new HashSet<Guid>();
        var random = new Random();

        var shuffledExercises = validExercises.OrderBy(_ => random.Next()).ToList();
        
        var exerciseQueue = new Queue<Domain.Entities.Exercise>(shuffledExercises);
        var usedExerciseIds = new HashSet<Guid>();

        while (exerciseQueue.Count > 0 && totalSeconds < request.TargetDurationMinutes * 60)
        {
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
                (sets * timePerSet) +
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
            usedEquipment.Add(exercise.Equipment);

            foreach (var groupId in exerciseMuscleGroupIds)
            {
                if (request.MuscleGroups.Contains(groupId))
                    coveredMuscleGroups.Add(groupId);
            }

            totalSeconds += totalTimeForExercise;
        }
        
        var workout = new Domain.Entities.Workout
        {
            UserId = request.UserId,
            Name = request.Name,
            Difficulty = request.Difficulty,
            TargetDurationMinutes = (int)Math.Round(totalSeconds / 60.0),
            Goal = request.Goal,
            MuscleGroups = requiredMuscleGroups,
            Equipment = usedEquipment,
            WorkoutExercises = selectedExercises,
        };

        await fitnessDbContext.Workouts.AddAsync(workout, cancellationToken);
        await fitnessDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}