using fitnessweb.Core.Queries;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.Muscle;

public class GetByIdMuscleQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetByIdMuscleQuery, Domain.Entities.Muscle>
{
    public async Task<Domain.Entities.Muscle> Handle(GetByIdMuscleQuery request, CancellationToken cancellationToken)
    {
        var muscle = await fitnessDbContext.Muscles
            .Include(m => m.MuscleGroup)
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        return muscle ?? throw new NullReferenceException("Muscle group not found");
    }
}