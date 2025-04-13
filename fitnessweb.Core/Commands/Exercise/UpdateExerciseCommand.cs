using fitnessweb.Domain.Entities;
using fitnessweb.Domain.Types;
using MediatR;

namespace fitnessweb.Core.Commands;

public class UpdateExerciseCommand : IRequest<Unit>
{
    public Guid ExerciseId { get; set; }
    public string? Name { get; set; }
    public Equipment? Equipment { get; set; }
    public int? SecondsPerSet { get; set; }
    public FitnessLevel? Difficulty { get; set; }
    public string? StartingPositionDescription { get; set; }
    public string? ExecutionDescription { get; set; }
    public List<String>? MuscleNames { get; set; }
    public string? ImagePath { get; set; }
}