using fitnessweb.Core.Queries;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.Muscle;

public class GetByIdMuscleQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetByIdMuscleQuery, Domain.Dtos.MuscleInfoDto>
{
    public async Task<Domain.Dtos.MuscleInfoDto> Handle(GetByIdMuscleQuery request, CancellationToken cancellationToken)
    {
        var muscle = await fitnessDbContext.Muscles
            .Include(m => m.MuscleGroup)
            .Select(m => new Domain.Dtos.MuscleInfoDto
            {
                Name = m.Name,
                Id = m.Id,
                MuscleGroupName = m.MuscleGroup.MuscleGroupName,
                MuscleGroupId = m.MuscleGroupId,
            })
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        return muscle ?? throw new NullReferenceException("Muscle not found");
    }
}