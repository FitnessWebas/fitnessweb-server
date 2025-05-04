using fitnessweb.Domain.Entities;

namespace fitnessweb.Core.Services.Interfaces;

public interface IJwtService
{
    public string GenerateJwtToken(User user);
    public Task<string> GenerateAndSaveRefreshToken(User user);
    
    public Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken);
}