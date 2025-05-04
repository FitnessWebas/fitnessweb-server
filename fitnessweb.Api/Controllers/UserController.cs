using fitnessweb.Core.Commands;
using fitnessweb.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace fitnessweb.Api.Controllers;

public class UserController : BaseController
{
    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
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

    [HttpPost("Authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}