namespace fitnessweb.Domain.Entities;

public class User : Entity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PhoneNumber { get; set; }
    public required DateTime Birthday { get; set; }
    public UserMetrics? Metric { get; set; }
}