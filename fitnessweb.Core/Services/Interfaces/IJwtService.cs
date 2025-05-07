using fitnessweb.Domain.Entities;

namespace fitnessweb.Core.Services.Interfaces;

public interface IJwtService
{
    public string GenerateJwtToken(User user);
}