using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Client.Command;
using CleanEvoBPM.Application.CQRS.Client.Query;
using CleanEvoBPM.Application.Models.Client;
using CleanEvoBPM.WebAPI.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanEvoBPM.WebAPI.Controllers
{
    [Authorize(Roles = UserRole.Admin)]
    public class ClientController : CustomBaseApiController
    {
        private readonly IUserHelper _userHelper;
        private readonly string userLogin;
        public ClientController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
            userLogin = userLogin = _userHelper != null ? _userHelper.GetUserName() : "";
        }

        [HttpGet]
        public async Task<IEnumerable<ClientResponseModel>> Get([FromQuery]FetchClientQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientResponseModel>> Get(Guid id)
        {
            var query = new GetClientDetailQuery() { Id = id };
            return await Mediator.Send(query);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<ClientResponseModel>> Get(string name)
        {
            name = !string.IsNullOrEmpty(name) ? name.Trim() : string.Empty;
            var query = new GetClientDetailQuery() { Name = name };
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Post(CreateClientCommand command)
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

        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponse>> Put(Guid id, [FromBody]UpdateClientCommand request)
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
            var query = new DeleteClientCommand() { Id = id, UpdatedBy = userLogin };
            return await Mediator.Send(query);
        }
    }
}
