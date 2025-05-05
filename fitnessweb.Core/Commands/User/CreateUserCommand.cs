using fitnessweb.Domain.Entities;
using MediatR;

namespace fitnessweb.Core.Commands;

public class CreateUserCommand : IRequest<bool>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Username { get; set; }
}