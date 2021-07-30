using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Command;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.CommandHandler;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Query;
using CleanEvoBPM.Application.Models.ProjectHolidayPlan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CleanEvoBPM.WebAPI.Controllers
{
    [Authorize(Roles = UserRole.Admin)]
    [Route("api/[controller]")]
    public class HolidayPlanController : CustomBaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Post(CreateProjectHolidayPlanCommand command)
        {
            var userId = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            command.CreatedBy = userId;
            command.LastUpdatedBy = userId;

            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectHolidayPlanResponseModel>> Get([FromQuery] GetProjectHolidayPlanQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponse>> Put(Guid id, [FromBody] UpdateProjectHolidayPlanCommand request)
        {
            request.Id = id;
            request.LastUpdatedBy = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            return await Mediator.Send(request);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponse>> Delete(Guid id)
        {
            return await Mediator.Send(new DeleteProjectHolidayPlanCommand() { Id = id });
        }
    }
}
