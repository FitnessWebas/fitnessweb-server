using MediatR;

namespace fitnessweb.Core.Commands;

public class VerifyPasswordCommand : IRequest<bool>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}