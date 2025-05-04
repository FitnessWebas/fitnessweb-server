using MediatR;

namespace fitnessweb.Core.Commands;

public class AuthenticateCommand : IRequest<string?>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}