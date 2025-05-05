using fitnessweb.Domain.Dtos;
using MediatR;

namespace fitnessweb.Core.Commands;

public class AuthenticateCommand : IRequest<AccessTokenUserIdDto?>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}