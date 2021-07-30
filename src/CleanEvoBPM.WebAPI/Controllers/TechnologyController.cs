using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Technology.Command;
using CleanEvoBPM.Application.CQRS.Technology.Query;
using CleanEvoBPM.Application.Models.Technology;
using CleanEvoBPM.WebAPI.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanEvoBPM.WebAPI.Controllers
{
    [Authorize(Roles = UserRole.Admin)]
    [Route("api/[controller]")]
    public class TechnologyController : CustomBaseApiController
    {
        private readonly IUserHelper _userHelper;
        private readonly string userLogin;
        public TechnologyController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
            userLogin = userLogin = _userHelper != null ? _userHelper.GetUserName() : "";
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Post(CreateTechnologyCommand command)
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
        public async Task<IEnumerable<TechnologyResponseModel>> Get([FromQuery] FetchTechnologyQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TechnologyResponseModel>> Get(Guid id)
        {
            var query = new GetTechnologyDetailsQuery() { Id = id };
            return await Mediator.Send(query);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<TechnologyResponseModel>> Get(string name)
        {
            name = !string.IsNullOrEmpty(name) ? name.Trim() : string.Empty;
            var query = new GetTechnologyDetailsQuery() { Name = name };
            return await Mediator.Send(query);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponse>> Put(Guid id, [FromBody] UpdateTechnologyCommand request)
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponse>> Delete(Guid id)
        {
            var query = new DeleteTechnologyCommand() { Id = id, UpdatedBy = userLogin };
            return await Mediator.Send(query);
        }
    }
}
