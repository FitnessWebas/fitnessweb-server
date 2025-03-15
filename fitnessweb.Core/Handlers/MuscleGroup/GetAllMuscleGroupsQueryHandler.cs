using fitnessweb.Core.Queries;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.MuscleGroup;

public class GetAllMuscleGroupsQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetAllMuscleGroupsQuery, List<Domain.Entities.MuscleGroup>>
{
    public async Task<List<Domain.Entities.MuscleGroup>> Handle(GetAllMuscleGroupsQuery request, CancellationToken cancellationToken)
    {
        return await fitnessDbContext.MuscleGroups
            .Include(mg => mg.Muscles)
            .ToListAsync(cancellationToken);
    }
}