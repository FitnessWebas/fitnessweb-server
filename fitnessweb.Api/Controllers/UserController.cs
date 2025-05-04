using fitnessweb.Core.Commands;
using fitnessweb.Core.Queries;
using fitnessweb.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fitnessweb.Api.Controllers;

public class UserController : BaseController
{
    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        var result = await Mediator.Send(command);

        if (result is false)
        {
            return BadRequest("User already exists");
        }
        
        return Ok("User created successfully");
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUsersQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([FromQuery] GetByIdUserQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPatch("Update")]
    public async Task<IActionResult> Update(UpdateUserCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateCommand command)
    {
        var result = await Mediator.Send(command);

        if (result is null)
        {
            return BadRequest("Wrong username or password");
        }
        
        SetRefreshTokenHttpOnly(result.RefreshToken);
        
        return Ok(result);
    }
    
    [HttpPost("Logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Append("refreshToken", string.Empty, new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddDays(-1),
            SameSite = SameSiteMode.None, // Strict or Lax
            Secure = false // true
        });

        return Ok("Logged out successfully.");
    }
    
    [AllowAnonymous]
    [HttpPost("RefreshJwtToken")]
    public async Task<IActionResult> RefreshJwtToken(GetRefreshJwtTokenQuery query)
    {
        var result = await Mediator.Send(query);

        if (result is null)
        {
            return Unauthorized("Invalid refresh token");
        }
        
        return Ok(result);
    }
    
    private void SetRefreshTokenHttpOnly(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddDays(JwtConstants.RefreshTokenExpiryInDays),
            SameSite = SameSiteMode.None, // Strict or Lax
            Secure = false // true
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}