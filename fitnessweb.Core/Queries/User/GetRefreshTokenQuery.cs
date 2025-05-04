using fitnessweb.Domain.Dtos;
using MediatR;

namespace fitnessweb.Core.Queries;

public class GetRefreshTokenQuery : IRequest<TokenResponseDto?>
{
    public Guid UserId { get; set; }
    public required string RefreshToken { get; set; }
}