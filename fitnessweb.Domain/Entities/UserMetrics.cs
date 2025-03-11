using fitnessweb.Domain.Types;

namespace fitnessweb.Domain.Entities;

public class UserMetrics : Entity
{
    public required int Height { get; set; }
    public required DateTime Birthday { get; set; }
    public required Gender Gender { get; set; }
    public required FitnessLevel FitnessLevel { get; set; }
    public Guid? UserId { get; set; }
    public User? User { get; set; }
}