using fitnessweb.Domain.Types;

namespace fitnessweb.Domain.Entities;

public class Exercise : Entity
{
    public required string Name { get; set; }
    public required Equipment Equipment { get; set; }
    public required int MinutesPerSet { get; set; }
    public required FitnessLevel Difficulty { get; set; }
    public required ICollection<Muscle> Muscles { get; set; }
    public required string ImagePath { get; set; }
}