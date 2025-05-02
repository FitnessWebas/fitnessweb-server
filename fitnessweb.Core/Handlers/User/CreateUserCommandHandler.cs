using fitnessweb.Core.Commands;
using fitnessweb.Core.Helpers;
using fitnessweb.Infrastructure;
using MediatR;

namespace fitnessweb.Core.Handlers.User;

public class CreateUserCommandHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<CreateUserCommand, Unit>
{
    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
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

        return Unit.Value;
    }
}