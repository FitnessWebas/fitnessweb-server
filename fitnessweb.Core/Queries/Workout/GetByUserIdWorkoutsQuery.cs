using fitnessweb.Domain.Dtos;
using MediatR;

namespace fitnessweb.Core.Queries;

public class GetByUserIdWorkoutsQuery : IRequest<List<WorkoutInfoDto>>
{
    public required Guid UserId { get; set; }
}