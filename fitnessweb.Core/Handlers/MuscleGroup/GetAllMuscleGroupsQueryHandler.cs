using fitnessweb.Core.Queries;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.MuscleGroup;

public class GetAllMuscleGroupsQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetAllMuscleGroupsQuery, List<Domain.Dtos.MuscleGroupInfoDto>>
{
    public async Task<List<Domain.Dtos.MuscleGroupInfoDto>> Handle(GetAllMuscleGroupsQuery request, CancellationToken cancellationToken)
    {
        return await fitnessDbContext.MuscleGroups
            .Include(mg => mg.Muscles)
            .Select(mg => new Domain.Dtos.MuscleGroupInfoDto
            {
                Name = mg.MuscleGroupName,
                Id = mg.Id,
                Muscles = mg.Muscles
                    .Select(ms => new Domain.Dtos.MuscleDto
                    {
                        Id = ms.Id,
                        Name = ms.Name
                    })
                    .ToList()
            })
            .ToListAsync(cancellationToken);
    }
}