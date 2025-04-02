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
        var muscles = await fitnessDbContext.Muscles
            .Where(m => request.MuscleNames.Contains(m.Name))
            .ToListAsync(cancellationToken);

        if (muscles.Count != request.MuscleNames.Count)
        {
            throw new Exception("Some muscles were not found in the database.");
        }
        
        var exerciseIds = request.Exercises.Select(e => e.ExerciseId).ToList();
        var exercises = await fitnessDbContext.Exercises
            .Where(e => exerciseIds.Contains(e.Id))
            .ToListAsync(cancellationToken);

        if (exercises.Count != request.Exercises.Count)
        {
            throw new Exception("Some exercises were not found in the database.");
        }


        var workoutExercises = request.Exercises.Select(dto => new WorkoutExercise
        {
            ExerciseId = dto.ExerciseId,
            Exercise = exercises.FirstOrDefault(e => e.Id == dto.ExerciseId),
            Sets = dto.Sets,
            RepsPerSet = dto.RepsPerSet
        }).ToList();
        
        var workout = new Domain.Entities.Workout
        {
            UserId = request.UserId,
            Name = request.Name,
            Difficulty = request.Difficulty,
            TargetDurationMinutes = request.TargetDurationMinutes,
            Goal = request.Goal,
            Muscles = muscles,
            Equipment = request.Equipment,
            WorkoutExercises = workoutExercises
        };
        
        await fitnessDbContext.Workouts.AddAsync(workout, cancellationToken);
        await fitnessDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}