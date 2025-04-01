using fitnessweb.Core.Commands;
using fitnessweb.Domain.Entities;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.Workout;

public class UpdateWorkoutCommandHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<UpdateWorkoutCommand, Unit>
{
    public async Task<Unit> Handle(UpdateWorkoutCommand command, CancellationToken cancellationToken)
    {
        var workout = await fitnessDbContext.Workouts
            .Include(w => w.WorkoutExercises)
            .FirstOrDefaultAsync(e => e.Id == command.WorkoutId, cancellationToken);
        
        if (workout == null)
            throw new Exception($"Workout with ID {command.WorkoutId} not found");
        
        if (command.UserId != null)
            workout.UserId = command.UserId.Value;
        
        if (!string.IsNullOrEmpty(command.Name))
            workout.Name = command.Name;
            
        if (command.Difficulty.HasValue)
            workout.Difficulty = command.Difficulty.Value;
        
        if (command.Goal.HasValue)
            workout.Goal = command.Goal.Value;
        
        if (command.TargetDurationMinutes.HasValue)
            workout.TargetDurationMinutes = command.TargetDurationMinutes.Value;
        
        if (command.Equipment != null && command.Equipment.Any())
            workout.Equipment = command.Equipment;
        
        if (command.MuscleNames != null && command.MuscleNames.Any())
        {
            var muscles = await fitnessDbContext.Muscles
                .Where(m => command.MuscleNames.Contains(m.Name))
                .ToListAsync(cancellationToken);

            if (muscles.Count != command.MuscleNames.Count)
                throw new Exception("Some muscles were not found in the database.");

            workout.Muscles = muscles;
        }
        
        if (command.WorkoutExercises != null && command.WorkoutExercises.Any())
        {
            var existingWorkoutExercises = workout.WorkoutExercises.ToList();
            var workoutExercisesToRemove = existingWorkoutExercises
                .Where(we => !command.WorkoutExercises.Select(dto => dto.ExerciseId).Contains(we.ExerciseId))
                .ToList();
            
            fitnessDbContext.WorkoutExercises.RemoveRange(workoutExercisesToRemove);
            
            foreach (var dto in command.WorkoutExercises)
            {
                var exercise = await fitnessDbContext.Exercises
                    .FirstOrDefaultAsync(e => e.Id == dto.ExerciseId, cancellationToken);
                
                if (exercise == null)
                    throw new Exception($"Exercise with ID {dto.ExerciseId} not found");
                
                var existingWorkoutExercise = existingWorkoutExercises
                    .FirstOrDefault(we => we.ExerciseId == dto.ExerciseId);
                
                if (existingWorkoutExercise != null)
                {
                    existingWorkoutExercise.Sets = dto.Sets;
                    existingWorkoutExercise.RepsPerSet = dto.RepsPerSet;
                }
                else
                {
                    var newWorkoutExercise = new WorkoutExercise
                    {
                        ExerciseId = dto.ExerciseId,
                        Exercise = exercise,
                        Sets = dto.Sets,
                        RepsPerSet = dto.RepsPerSet
                    };
                    workout.WorkoutExercises.Add(newWorkoutExercise);
                    fitnessDbContext.WorkoutExercises.Add(newWorkoutExercise);
                }
            }
        }
        await fitnessDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}