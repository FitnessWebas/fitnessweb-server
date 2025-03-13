using fitnessweb.Core.Commands;
using fitnessweb.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace fitnessweb.Api.Controllers;

public class UserMetricsController : BaseController
{
    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateUserMetricsCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("GetByUserId")]
    public async Task<IActionResult> GetById([FromQuery] GetByUserIdUserMetricsQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPatch("Update")]
    public async Task<IActionResult> Update(UpdateUserMetricsCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}