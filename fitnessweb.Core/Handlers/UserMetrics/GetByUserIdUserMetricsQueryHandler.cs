using fitnessweb.Core.Queries;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.UserMetrics;

public class GetByUserIdUserMetricsQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetByUserIdUserMetricsQuery, Domain.Entities.UserMetrics>
{
    public async Task<Domain.Entities.UserMetrics> Handle(GetByUserIdUserMetricsQuery request, CancellationToken cancellationToken)
    {
        return await fitnessDbContext.UsersMetrics
            .FirstAsync(userMetric => userMetric.UserId == request.UserId, cancellationToken);
    }
}