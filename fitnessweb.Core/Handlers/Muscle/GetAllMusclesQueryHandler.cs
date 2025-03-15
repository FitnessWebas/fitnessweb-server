using fitnessweb.Core.Queries;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.Muscle;

public class GetAllMusclesQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetAllMusclesQuery, List<Domain.Entities.Muscle>>
{
    public async Task<List<Domain.Entities.Muscle>> Handle(GetAllMusclesQuery request, CancellationToken cancellationToken)
    {
        return await fitnessDbContext.Muscles
            .Include(m => m.MuscleGroup)
            .ToListAsync(cancellationToken);
    }
}