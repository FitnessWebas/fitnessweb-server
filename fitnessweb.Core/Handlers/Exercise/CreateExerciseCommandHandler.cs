using fitnessweb.Core.Commands;
using fitnessweb.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace fitnessweb.Core.Handlers.Exercise;

public class CreateExerciseCommandHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<CreateExerciseCommand, Unit>
{
    public async Task<Unit> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        var muscles = await fitnessDbContext.Muscles
            .Where(m => request.MuscleNames.Contains(m.Name))
            .ToListAsync(cancellationToken);

        if (muscles.Count != request.MuscleNames.Count)
        {
            throw new Exception("Some muscles were not found in the database.");
        }

        var exercise = new Domain.Entities.Exercise
        {
            Name = request.Name,
            Equipment = request.Equipment,
            SecondsPerSet = request.SecondsPerSet,
            Difficulty = request.Difficulty,
            StartingPositionDescription = request.StartingPositionDescription,
            ExecutionDescription = request.ExecutionDescription,
            Muscles = muscles,
            ImagePath = request.ImagePath
        };

        await fitnessDbContext.Exercises.AddAsync(exercise, cancellationToken);
        await fitnessDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}