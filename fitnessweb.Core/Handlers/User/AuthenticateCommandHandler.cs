using fitnessweb.Core.Commands;
using fitnessweb.Core.Helpers;
using fitnessweb.Core.Services.Interfaces;
using fitnessweb.Domain.Dtos;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.User;

public class AuthenticateCommandHandler(
    FitnessWebDbContext fitnessDbContext,
    IJwtService jwtService) : IRequestHandler<AuthenticateCommand, TokenResponseDto?>
{
    public async Task<TokenResponseDto?> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var user = await fitnessDbContext.Users.
            FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

        if (user is null || PasswordHasher.Verify(request.Password, user.Password) is false)
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