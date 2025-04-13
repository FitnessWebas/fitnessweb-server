using fitnessweb.Domain.Entities;
using fitnessweb.Domain.Types;
using MediatR;

namespace fitnessweb.Core.Commands;

public class CreateExerciseCommand : IRequest<Unit>
{
    public required string Name { get; set; }
    public required Equipment Equipment { get; set; }
    public required int SecondsPerSet { get; set; }
    public required FitnessLevel Difficulty { get; set; }
    public required string StartingPositionDescription { get; set; }
    public required string ExecutionDescription { get; set; }
    public required List<String> MuscleNames { get; set; }
    public string? ImagePath { get; set; }
}