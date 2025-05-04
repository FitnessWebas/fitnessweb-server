using fitnessweb.Core.Queries;
using fitnessweb.Core.Services.Interfaces;
using fitnessweb.Domain.Dtos;
using MediatR;

namespace fitnessweb.Core.Handlers.User;

public class GetRefreshTokensQueryHandler(IJwtService jwtService) : IRequestHandler<GetRefreshTokenQuery, TokenResponseDto?>
{
    public async Task<TokenResponseDto?> Handle(GetRefreshTokenQuery query, CancellationToken cancellationToken)
    {
        var user = await jwtService.ValidateRefreshTokenAsync(query.UserId, query.RefreshToken);

        if (user is null)
        {
            return null;
        }

        var response = new TokenResponseDto
        {
            AccessToken = jwtService.GenerateJwtToken(user),
            RefreshToken = await jwtService.GenerateAndSaveRefreshToken(user)
        };

        return response;
    }
}