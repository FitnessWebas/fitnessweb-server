using fitnessweb.Domain.Entities;
using fitnessweb.Domain.Types;

namespace fitnessweb.Domain.Dtos;

public class WorkoutExerciseDto
{
    public Guid ExerciseId { get; set; }
    public string? ExerciseName { get; set; }
    public Equipment? Equipment { get; set; }
    public ICollection<MuscleDto>? Muscles { get; set; }
    public int Sets { get; set; }
    public int RepsPerSet { get; set; }
}