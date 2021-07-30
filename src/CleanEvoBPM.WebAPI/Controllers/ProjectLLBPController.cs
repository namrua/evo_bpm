using System.Collections.Generic;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.ProjectLLBP.Query;
using CleanEvoBPM.Application.CQRS.ProjectMilestone.Query;
using CleanEvoBPM.Application.Models.ProjectLLBP;
using Microsoft.AspNetCore.Mvc;

namespace CleanEvoBPM.WebAPI.Controllers
{
    public class ProjectLLBPController : CustomBaseApiController
    {
        [HttpGet]
        public async Task<IEnumerable<ProjectLLBPResponseModel>> Get([FromQuery] ProjectLLBPQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
