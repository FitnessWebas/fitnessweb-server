using fitnessweb.Domain.Dtos;
using MediatR;

namespace fitnessweb.Core.Queries;

public class GetByIdWorkoutQuery : IRequest<WorkoutInfoDto>
{
    public required Guid Id { get; set; }
}