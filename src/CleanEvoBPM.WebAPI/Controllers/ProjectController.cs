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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;

namespace CleanEvoBPM.WebAPI.Controllers
{
    // [Authorize(Roles = UserRole.Admin)]
    [Route("api/[controller]")]
    public class ProjectController : CustomBaseApiController
    {
        private readonly IUserHelper _userHelper;
        private readonly string userLogin;

        public ProjectController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
            userLogin = userLogin = _userHelper != null ? _userHelper.GetUserName() : "";
        }
        // POST
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Post(CreateProjectCommand command)
        {
            command.CreatedBy = userLogin;
            return await Mediator.Send(command);
        }

        //GET
        [HttpGet]
        public async Task<IEnumerable<ProjectResponseModel>> Get([FromQuery] FetchProjectQuery request)
        {
            var query = new FetchProjectQuery() { Search = request.Search };
            return await Mediator.Send(query);
        }

        //GET BY ID
        [HttpGet("{id}")]
        public async Task<GenericResponse<ProjectResponseByIdModel>> Get(Guid id)
        {
            var query = new GetProjectById() { Id = id };
            return await Mediator.Send(query);
        }

        //PUT 
        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponse>> Put(Guid id, [FromBody] UpdateProjectCommand request)
        {
            request.Id = id;
            request.UpdatedBy = userLogin;
            return await Mediator.Send(request);
        }

        //DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var query = new DeleteProjectCommand() { ProjectID = id , UpdatedBy = userLogin};
            return await Mediator.Send(query);
        }

        [HttpGet("Export")]
        public async Task<IActionResult> Export([FromQuery] ExportProject request, CancellationToken cancellationToken)
        {
            var listProject = await Mediator.Send(request);

            var dataTable = ExcelExportHelper
            .ListToDataTable<ProjectExportModel>(listProject.ToList());

            var colunmHeaders = new List<string>()
            {
                "#",
                "Project Code",
                "Project Name",
                "Client Name",
                "Project Type",
                "Service Type",
                "Methodology",
                "Business Domain",
                "Created on",
                "Last Modified",
                "Status"
            };

            byte[] fileContent = ExcelExportHelper.ExportExcel(dataTable, colunmHeaders, "", true);

            var fileName = "ProjectList.xlsx";

            return File(fileContent, ExcelExportHelper.ExcelContentType, fileName);
        }

        [HttpGet]
        [Route("my-projects")]
        public async Task<IEnumerable<ProjectResponseModel>> GetMyProjects()
        {
            var userId = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            return await Mediator.Send(new GetProjectsByManagerQuery(userId));
        }

        [HttpGet]
        [Route("my-projects/{id}")]
        public async Task<IActionResult> GetMyProjectById(Guid id)
        {
            var userId = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var result = await Mediator.Send(new GetMyProjectByIdQuery(userId, id));

            if (result == null)
                return new EmptyResult();

            if (result.Code == 200)
                return Ok(result);

            return new EmptyResult();
        }

        [HttpGet]
        [Route("my-project-resource/{id}")]
        public async Task<IEnumerable<ResourceResponseModel>> GetResourcesByProjectId(Guid id)
        {
            var result = await Mediator.Send(new FetchResourceQuery { id = id });
            return result;
        }

        // create a new resource for project
        [HttpPost]
        [Route("my-project-resource")]
        public async Task<ActionResult<GenericResponse>> CreateResource(CreateResourceCommand command)
        {
            var result =  await Mediator.Send(command);
            return result;
        }

        [HttpGet]
        [Route("status-count")]
        public async Task<IEnumerable<ProjectCountByStatusModel>> GetProjectCountByStatus()
        {
            var result = await Mediator.Send(new GetProjectCountByStatusQuery());
            return result;
        }
    }
}
