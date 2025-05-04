using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using fitnessweb.Core.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace fitnessweb.Core.Services;

public class JwtService(IConfiguration configuration) : IJwtService
{
    public string GenerateJwtToken(Domain.Entities.User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username)
        };
        
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtSettings:SecretToken")!));
        
        var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("JwtSettings:Issuer"),
            audience: configuration.GetValue<string>("JwtSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512)
        );
        
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}