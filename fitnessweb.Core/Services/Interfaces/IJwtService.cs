namespace fitnessweb.Core.Services.Interfaces;

public interface IJwtService
{
    public string GenerateJwtToken(Domain.Entities.User user);
}