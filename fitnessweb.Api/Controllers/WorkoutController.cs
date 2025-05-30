﻿using fitnessweb.Core.Commands;
using fitnessweb.Core.Queries;
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

    [HttpPost("Generate")]
    public async Task<IActionResult> Generate(GenerateWorkoutCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllWorkoutsQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([FromQuery] GetByIdWorkoutQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetByUserId")]
    public async Task<IActionResult> GetById([FromQuery] GetByUserIdWorkoutsQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPatch("Update")]
    public async Task<IActionResult> Update(UpdateWorkoutCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}