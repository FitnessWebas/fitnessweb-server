using fitnessweb.Core.Commands;
using fitnessweb.Core.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fitnessweb.Api.Controllers;

public class UserController : BaseController
{
    [AllowAnonymous]
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
    
    [AllowAnonymous]
    [HttpGet("GetByEmail")]
    public async Task<IActionResult> GetByEmail([FromQuery] GetByEmailUserQuery query)
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
        
        return Ok(result);
    }
}