using fitnessweb.Core.Commands;
using fitnessweb.Core.Helpers;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.User;

public class CreateUserCommandHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<CreateUserCommand, bool>
{
    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await fitnessDbContext.Users.AnyAsync(user => user.Username == request.Username, cancellationToken))
        {
            return false;
        }
        
        string hashedPassword = PasswordHasher.Hash(request.Password);

        var user = new Domain.Entities.User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Username = request.Username,
            Password = hashedPassword
        };

        await fitnessDbContext.Users.AddAsync(user, cancellationToken);
        await fitnessDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}