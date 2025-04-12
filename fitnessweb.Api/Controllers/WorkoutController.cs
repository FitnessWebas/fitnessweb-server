using System.Text.Json;
using System.Text.Json.Serialization;
using fitnessweb.Core.Commands;
using fitnessweb.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace fitnessweb.Api.Controllers;

public class WorkoutController : BaseController
{
    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateWorkoutCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllWorkoutsQuery query)
    {
        var result = await Mediator.Send(query);
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        var json = JsonSerializer.Serialize(result, options);

        return Content(json, "application/json");
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([FromQuery] GetByIdWorkoutQuery query)
    {
        var result = await Mediator.Send(query);
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        var json = JsonSerializer.Serialize(result, options);

        return Content(json, "application/json");
    }
    
    [HttpPatch("Update")]
    public async Task<IActionResult> Update(UpdateWorkoutCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}