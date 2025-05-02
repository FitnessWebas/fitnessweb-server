using fitnessweb.Core.Commands;
using fitnessweb.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace fitnessweb.Api.Controllers
{
    public class MuscleGroupController : BaseController
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllMuscleGroupsQuery query)
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
        public async Task<IActionResult> GetById([FromQuery] GetByIdMuscleGroupQuery query)
        {
            var result = await Mediator.Send(query);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            var json = JsonSerializer.Serialize(result, options);

            return Content(json, "application/json");
        }
    }
}