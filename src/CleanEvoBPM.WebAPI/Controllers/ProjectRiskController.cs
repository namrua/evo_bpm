using System.Collections.Generic;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.ProjectRisk.Query;
using CleanEvoBPM.Application.Models.ProjectRisk;
using Microsoft.AspNetCore.Mvc;

namespace CleanEvoBPM.WebAPI.Controllers
{
    public class ProjectRiskController : CustomBaseApiController
    {
        [HttpGet]
        public async Task<IEnumerable<ProjectRiskModel>> Get([FromQuery] FetchProjectRiskQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}