using fitnessweb.Domain.Dtos;
using fitnessweb.Domain.Entities;
using fitnessweb.Domain.Types;
using MediatR;

namespace fitnessweb.Core.Commands;

public class UpdateWorkoutCommand : IRequest<Unit>
{
    public Guid WorkoutId { get; set; }
    public Guid? UserId { get; set; }
    public string? Name { get; set; }
    public Goal? Goal { get; set; }
    public ICollection<WorkoutExerciseDto>? WorkoutExercises { get; set; }
}