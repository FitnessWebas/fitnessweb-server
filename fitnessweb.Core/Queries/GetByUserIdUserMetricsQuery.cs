using fitnessweb.Domain.Entities;
using MediatR;

namespace fitnessweb.Core.Queries;

public class GetByUserIdUserMetricsQuery : IRequest<UserMetrics>
{
    public required Guid UserId { get; set; }
}