using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Project.Command;
using CleanEvoBPM.Application.CQRS.Project.Query;
using CleanEvoBPM.Application.CQRS.Resource.Command;
using CleanEvoBPM.Application.CQRS.Resource.Query;
using CleanEvoBPM.Application.Models.Project;
using CleanEvoBPM.Application.Models.Resource;
using CleanEvoBPM.WebAPI.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
namespace CleanEvoBPM.WebAPI.Controllers
{

    [Authorize(Roles = UserRole.Admin)]
    [Route("api/[controller]")]
    public class ResourceController : CustomBaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IEnumerable<ResourceResponseModel>> GetResourcesByProjectId(Guid id)
        {
            var result = await Mediator.Send(new FetchResourceQuery { id = id });
            return result;
        }

        // create a new resource for project
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> CreateResource(CreateResourceCommand command)
        {
            var result = await Mediator.Send(command);
            return result;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponse>> UpdateResource(Guid id, [FromBody] UpdateResourceCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command);
            return result;
        }
        [HttpGet]
        [Route("detail")]
        public async Task<ResourceResponseModel> Get([FromQuery] GetResourceDetailQuery query)
        {
            var result = await Mediator.Send(query);
            return result;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponse>> Delete(Guid id)
        {

            var result = await Mediator.Send(new DeleteResourceCommand { Id = id});
            return result;
        }
    }
}
