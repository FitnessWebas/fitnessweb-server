namespace fitnessweb.Domain.Dtos;

public class AccessTokenUserIdDto
{
    public required string AccessToken { get; set; }
    public Guid UserId { get; set; }
}