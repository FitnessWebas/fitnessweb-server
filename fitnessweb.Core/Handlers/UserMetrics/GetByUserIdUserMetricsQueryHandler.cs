using fitnessweb.Core.Queries;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.UserMetrics;

public class GetByUserIdUserMetricsQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetByUserIdUserMetricsQuery, Domain.Entities.UserMetrics?>
{
    public async Task<Domain.Entities.UserMetrics?> Handle(GetByUserIdUserMetricsQuery request, CancellationToken cancellationToken)
    {
        if(request.UserId == Guid.Empty)
            throw new ArgumentException($"No UserId provided.");
        
        var user = await fitnessDbContext.UsersMetrics.FirstOrDefaultAsync(userMetric => userMetric.UserId == request.UserId, cancellationToken);

        if (user == null)
            return user;
            //throw new Exception($"UserMetrics for userId \"{request.UserId}\" doesn't exist.");
        
        return await fitnessDbContext.UsersMetrics
            .FirstAsync(userMetric => userMetric.UserId == request.UserId, cancellationToken);
    }
}