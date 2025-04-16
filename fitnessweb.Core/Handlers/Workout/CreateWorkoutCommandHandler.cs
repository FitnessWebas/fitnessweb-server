using fitnessweb.Core.Commands;
using fitnessweb.Domain.Entities;
using fitnessweb.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace fitnessweb.Core.Handlers.Workout;

public class CreateWorkoutCommandHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<CreateWorkoutCommand, Unit>
{
    public async Task<Unit> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var exerciseIds = request.Exercises.Select(e => e.ExerciseId).ToList();
        
        var exercises = await fitnessDbContext.Exercises
            .Include(e => e.Muscles)
            .ThenInclude(e => e.MuscleGroup)
            .Where(e => exerciseIds.Contains(e.Id))
            .ToListAsync(cancellationToken);

        if (exercises.Count != request.Exercises.Count)
        {
            throw new Exception("Some exercises were not found in the database.");
        }

        var muscleGroups = exercises
            .SelectMany(e => e.Muscles)
            .Select(m => m.MuscleGroup)
            .Distinct()
            .ToList();
        
        
        var workoutExercises = request.Exercises.Select(dto => new WorkoutExercise
        {
            ExerciseId = dto.ExerciseId,
            Exercise = exercises.FirstOrDefault(e => e.Id == dto.ExerciseId),
            Sets = dto.Sets,
            RepsPerSet = dto.RepsPerSet
        }).ToList();
        
        var difficulty = exercises.Max(e => e.Difficulty);
        var equipment = exercises
            .Select(e => e.Equipment)
            .Distinct()
            .ToList();

        var duration = workoutExercises.Sum(workoutExercise =>
        {
            var exercise = workoutExercise.Exercise;
            return exercise != null ? workoutExercise.Sets * exercise.SecondsPerSet : 0;
        }) / 60;
        
        var workout = new Domain.Entities.Workout
        {
            UserId = request.UserId,
            Name = request.Name,
            Difficulty = difficulty,
            TargetDurationMinutes = duration,
            Goal = request.Goal,
            MuscleGroups = muscleGroups,
            Equipment = equipment,
            WorkoutExercises = workoutExercises
        };
        
        await fitnessDbContext.Workouts.AddAsync(workout, cancellationToken);
        await fitnessDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}