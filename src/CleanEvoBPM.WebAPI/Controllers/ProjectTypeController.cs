using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CleanEvoBPM.Application.CQRS.ProjectType.Command;
using CleanEvoBPM.Application.CQRS.ProjectType.Query;
using CleanEvoBPM.Application.Models.ProjectType;
using CleanEvoBPM.Application.Common;
using Microsoft.AspNetCore.Authorization;
using CleanEvoBPM.WebAPI.Helper;

namespace CleanEvoBPM.WebAPI.Controllers
{
    [Authorize(Roles = UserRole.Admin)]
    public class ProjectTypeController : CustomBaseApiController
    {
        private readonly IUserHelper _userHelper;
        private readonly string userLogin;
        public ProjectTypeController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
            userLogin = userLogin = _userHelper != null ? _userHelper.GetUserName() : "";
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Post(CreateProjectTypeCommand command)
        {
            try
            {
                command.CreatedBy = userLogin;
                return await Mediator.Send(command);
            }
            catch (Exception ex)
            {

                var error = GenericResponse.CustomValidationExceptionResult(ex);
                return error;
            }
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectTypeResponseModel>> Get([FromQuery] GetProjectTypeQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTypeResponseModel>> Get(Guid id)
        {
            var query = new GetProjectTypeDetailsQuery() { Id = id };
            return await Mediator.Send(query);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<ProjectTypeResponseModel>> Get(string name)
        {
            name = !string.IsNullOrEmpty(name) ? name.Trim() : string.Empty;
            var query = new GetProjectTypeDetailsQuery() { Name = name };
            return await Mediator.Send(query);
        }

        //PUT 
        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponse>> Put(Guid id, [FromBody] UpdateProjectTypeCommand request)
        {
            try
            {
                request.Id = id;
                request.UpdatedBy = userLogin;
                return await Mediator.Send(request);
            }
            catch (Exception ex)
            {

                var error = GenericResponse.CustomValidationExceptionResult(ex);
                return error;
            }
        }

        //DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponse>> Delete(Guid id)
        {
            var query = new DeleteProjectTypeCommand() { Id = id, UpdatedBy = userLogin };
            return await Mediator.Send(query);
        }
    }
}
