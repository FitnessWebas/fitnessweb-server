namespace fitnessweb.Domain.Entities;

public class User : Entity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Username { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public UserMetrics? Metric { get; set; }
    public ICollection<Workout>? Workouts { get; set; }
}