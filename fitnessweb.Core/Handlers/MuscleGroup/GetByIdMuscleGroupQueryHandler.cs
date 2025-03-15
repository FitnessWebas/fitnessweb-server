using fitnessweb.Core.Queries;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.MuscleGroup;

public class GetByIdMuscleGroupQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetByIdMuscleGroupQuery, Domain.Dtos.MuscleGroupInfoDto>
{
    public async Task<Domain.Dtos.MuscleGroupInfoDto> Handle(GetByIdMuscleGroupQuery request, CancellationToken cancellationToken)
    {
        var muscleGroup = await fitnessDbContext.MuscleGroups
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
            .FirstOrDefaultAsync(mg => mg.Id == request.Id, cancellationToken);

        return muscleGroup ?? throw new NullReferenceException("Muscle group not found");
    }
}