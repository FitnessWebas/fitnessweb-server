namespace fitnessweb.Domain.Entities;

public class Muscle : Entity
{
    public MuscleGroup MuscleGroup { get; set; }
    public required string Name { get; set; }
    public Guid MuscleGroupId { get; set; }
    
    public ICollection<Exercise>? Exercises { get; set; }
}