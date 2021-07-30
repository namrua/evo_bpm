using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProblemCategory.Command;
using CleanEvoBPM.Application.CQRS.ProblemCategory.Query;
using CleanEvoBPM.Application.CQRS.Technology.Command;
using CleanEvoBPM.Application.CQRS.Technology.Query;
using CleanEvoBPM.Application.Models.ProblemCategory;
using CleanEvoBPM.Application.Models.Technology;
using CleanEvoBPM.WebAPI.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CleanEvoBPM.WebAPI.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    [Route("api/[controller]")]
    public class ProblemCategoryController:CustomBaseApiController
    {
        private readonly IUserHelper _userHelper;
        private readonly string userLogin;
        public ProblemCategoryController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
            userLogin = _userHelper.GetUserName();
        }

        [HttpGet]
        public async Task<IEnumerable<ProblemCategoryResponseModel>> Get([FromQuery] FetchProblemCategoryQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<ProblemCategoryResponseModel>> Get(string name)
        {
            name = !string.IsNullOrEmpty(name) ? name.Trim() : string.Empty;
            var query = new GetProblemCategoryDetailQuery() { Name = name };
            return await Mediator.Send(query);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> Post(CreateProblemCategoryCommand command)
        {
            command.CreatedBy = userLogin;
            return await Mediator.Send(command);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProblemCategoryResponseModel>> Get(Guid id)
        {
            var query = new GetProblemCategoryDetailQuery() { Id = id };
            return await Mediator.Send(query);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] UpdateProblemCategoryCommand request)
        {
            request.Id = id;
            request.UpdatedBy = userLogin;
            return await Mediator.Send(request);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponse>> Delete(Guid id)
        {
            var query = new DeleteProblemCategoryCommand() { Id = id, UpdatedBy = userLogin };
            return await Mediator.Send(query);
        }
    }
}
