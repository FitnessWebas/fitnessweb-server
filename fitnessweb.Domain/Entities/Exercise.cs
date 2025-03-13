using fitnessweb.Domain.Types;

namespace fitnessweb.Domain.Entities;

public class Exercise : Entity
{
    public required string Name { get; set; }
    public required Equipment Equipment { get; set; }
    public required int Time { get; set; }
    public required string Difficulty { get; set; }
    public required ICollection<Muscle> Muscles { get; set; }
}