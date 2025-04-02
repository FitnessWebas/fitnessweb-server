using fitnessweb.Domain.Entities;
using MediatR;

namespace fitnessweb.Core.Queries;

public class GetByIdUserQuery : IRequest<User>
{
    public required Guid Id { get; set; }
}