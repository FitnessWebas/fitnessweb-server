using fitnessweb.Domain.Entities;
using fitnessweb.Domain.Types;
using MediatR;

namespace fitnessweb.Core.Commands;

public class GenerateWorkoutCommand : IRequest<Unit>
{
    public required Guid UserId { get; set; }
    public required string Name { get; set; }
    public required int TargetDurationMinutes { get; set; }
    public required Goal Goal { get; set; }
    public required FitnessLevel Difficulty { get; set; }
    public required ICollection<Equipment> Equipment { get; set; }
    public required ICollection<Guid> MuscleGroups { get; set; }
}