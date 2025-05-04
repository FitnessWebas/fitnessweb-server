using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using fitnessweb.Core.Services.Interfaces;
using fitnessweb.Domain.Entities;
using fitnessweb.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using JwtConstants = fitnessweb.Domain.Constants.JwtConstants;

namespace fitnessweb.Core.Services;

public class JwtService(FitnessWebDbContext fitnessDbContext, 
    IConfiguration configuration) : IJwtService
{
    public string GenerateJwtToken(User user)
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
            expires: DateTime.UtcNow.AddMinutes(JwtConstants.JwtTokenExpiryInMinutes),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512)
        );
        
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public async Task<string> GenerateAndSaveRefreshToken(User user)
    {
        var refreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(JwtConstants.RefreshTokenExpiryInDays);
        
        await fitnessDbContext.SaveChangesAsync();
        return refreshToken;
    }

    public async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
    {
        var user = await fitnessDbContext.Users.FindAsync(userId);

        if (user is null || user.RefreshToken != refreshToken
                         || user.RefreshTokenExpiryTime < DateTime.UtcNow)
        {
            return null;
        }
        
        return user;
    }
}