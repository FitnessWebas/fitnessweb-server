using fitnessweb.Core.Commands;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.Exercise;

public class UpdateExerciseCommandHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<UpdateExerciseCommand, Unit>
{
    public async Task<Unit> Handle(UpdateExerciseCommand command, CancellationToken cancellationToken)
    {
        var exercise = await fitnessDbContext.Exercises
            .FirstOrDefaultAsync(e => e.Id == command.ExerciseId, cancellationToken);
        
        if (exercise == null)
            throw new Exception($"Exercise with ID {command.ExerciseId} not found");
        
        if (!string.IsNullOrEmpty(command.Name))
            exercise.Name = command.Name;
            
        if (command.Equipment.HasValue)
            exercise.Equipment = command.Equipment.Value;
            
        if (command.MinutesPerSet.HasValue)
            exercise.MinutesPerSet = command.MinutesPerSet.Value;
            
        if (command.Difficulty.HasValue)
            exercise.Difficulty = command.Difficulty.Value;
        
        if (!string.IsNullOrEmpty(command.StartingPositionDescription))
            exercise.StartingPositionDescription = command.StartingPositionDescription;
        
        if (!string.IsNullOrEmpty(command.ExecutionDescription))
            exercise.ExecutionDescription = command.ExecutionDescription;
            
        if (command.MuscleNames != null && command.MuscleNames.Any())
        {
            var muscles = await fitnessDbContext.Muscles
                .Where(m => command.MuscleNames.Contains(m.Name))
                .ToListAsync(cancellationToken);

            if (muscles.Count != command.MuscleNames.Count)
                throw new Exception("Some muscles were not found in the database.");

            exercise.Muscles = muscles;
        }
        
        if (!string.IsNullOrEmpty(command.ImagePath))
            exercise.ImagePath = command.ImagePath;
        

        await fitnessDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}