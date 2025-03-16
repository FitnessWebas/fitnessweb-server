namespace fitnessweb.Domain.Entities;

public class WorkoutExercise : Entity
{
    public required Workout Workout { get; set; }
    public required Exercise Exercise { get; set; }
    
    public required int Sets { get; set; }
    public required int RepsPerSet { get; set; }
}