using MediatR;

namespace fitnessweb.Core.Queries;

public class GetRefreshJwtTokenQuery : IRequest<string?>
{
    public Guid UserId { get; set; }
    public required string RefreshToken { get; set; }
}