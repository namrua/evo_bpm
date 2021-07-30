using System.Collections.Generic;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.ProjectMilestone.Query;
using CleanEvoBPM.Application.Models.ProjectMilestone;
using Microsoft.AspNetCore.Mvc;

namespace CleanEvoBPM.WebAPI.Controllers
{
    public class ProjectMilestoneController : CustomBaseApiController
    {
        [HttpGet]
        public async Task<IEnumerable<ProjectMilestoneModel>> Get([FromQuery] FetchProjectMilestoneQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
