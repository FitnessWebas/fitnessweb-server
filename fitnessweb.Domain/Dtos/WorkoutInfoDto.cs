using fitnessweb.Domain.Entities;
using fitnessweb.Domain.Types;

namespace fitnessweb.Domain.Dtos;

public class WorkoutInfoDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime DateOfCreation { get; set; }
    public string Name { get; set; }
    public FitnessLevel Difficulty { get; set; }
    public ICollection<Equipment> Equipment { get; set; }
    public int TargetDurationMinutes { get; set; }
    public Goal Goal { get; set; }
    public ICollection<MuscleGroupDto> MuscleGroups { get; set; }
    public ICollection<WorkoutExerciseDto> WorkoutExercises { get; set; }
}