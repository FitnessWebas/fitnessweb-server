using fitnessweb.Core.Queries;
using fitnessweb.Core.Services.Interfaces;
using fitnessweb.Domain.Dtos;
using MediatR;

namespace fitnessweb.Core.Handlers.User;

public class GetRefreshJwtTokenQueryHandler(IJwtService jwtService) : IRequestHandler<GetRefreshJwtTokenQuery, string?>
{
    public async Task<string?> Handle(GetRefreshJwtTokenQuery query, CancellationToken cancellationToken)
    {
        var user = await jwtService.ValidateRefreshTokenAsync(query.UserId, query.RefreshToken);

        return user is null ? null : jwtService.GenerateJwtToken(user);
    }
}