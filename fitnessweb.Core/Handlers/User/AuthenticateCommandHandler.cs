using fitnessweb.Core.Commands;
using fitnessweb.Core.Helpers;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.User;

public class AuthenticateCommandHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<AuthenticateCommand, bool>
{
    public async Task<bool> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var user = await fitnessDbContext.Users.
            FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid username.");
        }

        return PasswordHasher.Verify(request.Password, user.Password);
    }
}