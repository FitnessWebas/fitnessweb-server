using fitnessweb.Domain.Dtos;
using fitnessweb.Domain.Entities;
using MediatR;

namespace fitnessweb.Core.Queries;

public class GetByIdUserQuery : IRequest<UserInfoDto>
{
    public required Guid UserId { get; set; }
}