using fitnessweb.Core.Queries;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.Muscle;

public class GetAllMusclesQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetAllMusclesQuery, List<Domain.Dtos.MuscleInfoDto>>
{
    public async Task<List<Domain.Dtos.MuscleInfoDto>> Handle(GetAllMusclesQuery request, CancellationToken cancellationToken)
    {
        return await fitnessDbContext.Muscles
            .Include(m => m.MuscleGroup)
            .Select(m => new Domain.Dtos.MuscleInfoDto
            {
                Name = m.Name,
                Id = m.Id,
                MuscleGroupName = m.MuscleGroup.MuscleGroupName,
                MuscleGroupId = m.MuscleGroupId,
            })
            .ToListAsync(cancellationToken);
    }
}

    
