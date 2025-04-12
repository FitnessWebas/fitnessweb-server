using fitnessweb.Domain.Types;

namespace fitnessweb.Domain.Entities;

public class Workout : Entity
{
    public required Guid UserId { get; set; }
    public required string Name { get; set; }
    public required FitnessLevel Difficulty { get; set; }
    public required ICollection<MuscleGroup> MuscleGroups { get; set; }
    public required int TargetDurationMinutes { get; set; }
    public required ICollection<Equipment> Equipment { get; set; }
    public required Goal Goal { get; set; }
    
    public required ICollection<WorkoutExercise> WorkoutExercises { get; set; }
}