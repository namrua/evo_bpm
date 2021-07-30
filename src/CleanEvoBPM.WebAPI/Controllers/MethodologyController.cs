using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Methodology.Command;
using CleanEvoBPM.Application.CQRS.Methodology.Query;
using CleanEvoBPM.Application.Models.Methodology;
using CleanEvoBPM.WebAPI.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanEvoBPM.WebAPI.Controllers
{
    [Authorize(Roles = UserRole.Admin)]
    public class MethodologyController : CustomBaseApiController
    {
        private readonly IUserHelper _userHelper;
        private readonly string userLogin;
        public MethodologyController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
            userLogin = userLogin = _userHelper != null ? _userHelper.GetUserName() : "";
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Post(CreateMethodologyCommand command)
        {
            command.CreatedBy = userLogin;
            return await Mediator.Send(command);
        }

        //GET
        [HttpGet]
        public async Task<IEnumerable<MethodologyResponseModel>> Get([FromQuery] GetMethodologyQuery query)
        {
            //var query = new FetchMethodologyQuery();
            return await Mediator.Send(query);
        }

        //PUT 
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] UpdateMethodologyCommand request)
        {
            request.Id = id;
            request.UpdatedBy = userLogin;
            return await Mediator.Send(request);
        }

        //DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponse>> Delete(Guid id)
        {
            var query = new DeleteMethodologyCommand() { Id = id, UpdatedBy = userLogin };
            return await Mediator.Send(query);
        }
    }
}
