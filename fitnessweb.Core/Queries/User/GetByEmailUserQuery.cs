using fitnessweb.Domain.Dtos;
using fitnessweb.Domain.Entities;
using MediatR;

namespace fitnessweb.Core.Queries;

public class GetByEmailUserQuery : IRequest<Guid>
{
    public required string Email { get; set; }
}