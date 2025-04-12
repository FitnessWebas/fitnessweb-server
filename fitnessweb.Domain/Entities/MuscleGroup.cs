namespace fitnessweb.Domain.Entities;

public class MuscleGroup : Entity
{
    public required string MuscleGroupName { get; set; }
    public ICollection<Muscle> Muscles { get; set; } = new List<Muscle>();
    public ICollection<Workout>? Workouts { get; set; }
}