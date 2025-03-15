using fitnessweb.Core.Queries;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.MuscleGroup;

public class GetByIdMuscleGroupQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetByIdMuscleGroupQuery, Domain.Entities.MuscleGroup>
{
    public async Task<Domain.Entities.MuscleGroup> Handle(GetByIdMuscleGroupQuery request, CancellationToken cancellationToken)
    {
        var muscleGroup = await fitnessDbContext.MuscleGroups
            .Include(mg => mg.Muscles)
            .FirstOrDefaultAsync(mg => mg.Id == request.Id, cancellationToken);

        return muscleGroup ?? throw new NullReferenceException("Muscle group not found");
    }
}