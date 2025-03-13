using fitnessweb.Domain.Types;

namespace fitnessweb.Domain.Entities;

public class Workout :Entity
{
    public required FitnessLevel Difficulty { get; set; }
    public required ICollection<Muscle> Muscles { get; set; }
    public required int DurationMinutes { get; set; }
    public required ICollection<Equipment> Equipment { get; set; }
    public required Goal Goal { get; set; }
}