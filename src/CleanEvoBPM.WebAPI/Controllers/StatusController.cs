using System;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Status.Command;
using CleanEvoBPM.Application.CQRS.Status.Query;
using CleanEvoBPM.Application.Models.Status;
using CleanEvoBPM.WebAPI.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanEvoBPM.WebAPI.Controllers
{
    [Authorize(Roles = UserRole.Admin)]
    public class StatusController : CustomBaseApiController
    {
        private readonly IUserHelper _userHelper;
        private readonly string userLogin;
        public StatusController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
            userLogin = _userHelper.GetUserName();
        }

        [HttpGet]
        public async Task<GenericListResponse<StatusResponseModel>> Get([FromQuery]FetchStatusQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<GenericResponse<StatusResponseModel>> Get(Guid id)
        {
            var query = new GetStatusDetailsQuery() {Id  = id};
            return await Mediator.Send(query);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Post(CreateStatusCommand command)
        {
            command.CreatedBy = userLogin;
            return await Mediator.Send(command);
        }

        //PUT TEST
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] UpdateStatusCommand request)
        {
            request.Id = id;
            request.UpdatedBy = userLogin;
            return await Mediator.Send(request);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponse>> Delete(Guid id)
        {
            var request = new DeleteStatusCommand {Id =id};
            return await Mediator.Send(request);
        }
    }
}