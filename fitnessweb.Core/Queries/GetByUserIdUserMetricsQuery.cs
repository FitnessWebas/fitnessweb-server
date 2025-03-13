using fitnessweb.Domain.Entities;
using MediatR;

namespace fitnessweb.Core.Queries;

public class GetByUserIdUserMetricsQuery : IRequest<UserMetrics>
{
    public Guid UserId { get; set; }
}