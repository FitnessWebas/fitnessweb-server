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
            .Include(W => W.MuscleGroups)
            .Include(w => w.WorkoutExercises)
            .FirstOrDefaultAsync(e => e.Id == command.WorkoutId, cancellationToken);
        
        if (workout == null)
            throw new Exception($"Workout with ID {command.WorkoutId} not found");
        
        if (command.UserId != null)
            workout.UserId = command.UserId.Value;
        
        if (!string.IsNullOrEmpty(command.Name))
            workout.Name = command.Name;
        
        if (command.Goal.HasValue)
            workout.Goal = command.Goal.Value;
        
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
            
            var updatedExerciseIds = command.WorkoutExercises.Select(dto => dto.ExerciseId).ToList();
            
            var updatedExercises = await fitnessDbContext.Exercises
                .Include(e => e.Muscles)
                .ThenInclude(m => m.MuscleGroup)
                .Where(e => updatedExerciseIds.Contains(e.Id))
                .ToListAsync(cancellationToken);
            
            var difficulty = updatedExercises.Max(e => e.Difficulty);
            
            var equipment = updatedExercises
                .Select(e => e.Equipment)
                .Distinct()
                .ToList();
            var duration = workout.WorkoutExercises.Sum(workoutExercise =>
            {
                var exercise = workoutExercise.Exercise;
                return exercise != null ? workoutExercise.Sets * exercise.SecondsPerSet : 0;
            }) / 60;
            
            var updatedMuscleGroups = updatedExercises
                .SelectMany(e => e.Muscles)
                .Select(m => m.MuscleGroup)
                .Where(mg => mg != null)
                .Distinct()
                .ToList();
            
            workout.MuscleGroups.Clear();
            foreach (var mg in updatedMuscleGroups)
            {
                workout.MuscleGroups.Add(mg);
            }
            
            workout.Difficulty = difficulty;
            workout.Equipment = equipment;
            workout.TargetDurationMinutes = duration;
        }
        await fitnessDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}