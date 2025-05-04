using MediatR;

namespace fitnessweb.Core.Commands;

public class AuthenticateCommand : IRequest<bool>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}