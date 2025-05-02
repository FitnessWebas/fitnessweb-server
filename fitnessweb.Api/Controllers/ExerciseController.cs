using fitnessweb.Core.Commands;
using fitnessweb.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace fitnessweb.Api.Controllers;

public class ExerciseController : BaseController
{
    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateExerciseCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllExercisesQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([FromQuery] GetByIdExerciseQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPatch("Update")]
    public async Task<IActionResult> Update(UpdateExerciseCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}