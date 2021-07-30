using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ServiceType.Command;
using CleanEvoBPM.Application.CQRS.ServiceType.Query;
using CleanEvoBPM.Application.Models.ServiceType;
using CleanEvoBPM.WebAPI.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanEvoBPM.WebAPI.Controllers
{
    [Authorize(Roles = UserRole.Admin)]
    public class ServiceTypeController : CustomBaseApiController
    {
        private readonly IUserHelper _userHelper;
        private readonly string userLogin;
        public ServiceTypeController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
            userLogin = userLogin = _userHelper != null ? _userHelper.GetUserName() : "";
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Post(CreateServiceTypeCommand command)
        {
            command.CreatedBy = userLogin;
            return await Mediator.Send(command);
        }

        //GET
        [HttpGet]
        public async Task<IEnumerable<ServiceTypeResponseModel>> Get([FromQuery] GetServiceTypeQuery query)
        {
            //var query = new FetchServiceTypeQuery();
            return await Mediator.Send(query);
        }

        //PUT 
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] UpdateServiceTypeCommand request)
        {
            request.Id = id;
            request.UpdatedBy = userLogin;
            return await Mediator.Send(request);
        }

        //DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponse>> Delete(Guid id)
        {
            var query = new DeleteServiceTypeCommand() { Id = id, UpdatedBy = userLogin };
            return await Mediator.Send(query);
        }
    }
}
