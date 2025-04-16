using fitnessweb.Domain.Dtos;
using fitnessweb.Domain.Entities;
using fitnessweb.Domain.Types;
using MediatR;

namespace fitnessweb.Core.Commands;

public class CreateWorkoutCommand : IRequest<Unit>
{
    public required Guid UserId { get; set; }
    public required string Name { get; set; }
    public required Goal Goal { get; set; }
    public required ICollection<WorkoutExerciseDto> Exercises { get; set; }
}