using fitnessweb.Core.Commands;
using fitnessweb.Core.Helpers;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.User;

public class VerifyPasswordCommandHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<VerifyPasswordCommand, bool>
{
    public async Task<bool> Handle(VerifyPasswordCommand request, CancellationToken cancellationToken)
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