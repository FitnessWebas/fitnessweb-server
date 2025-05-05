namespace fitnessweb.Domain.Dtos;

public class TokenResponseWithUserIdDto
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
    public Guid UserId { get; set; }
}