using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Command;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Query;
using CleanEvoBPM.Application.Models.BusinessDomain;
using CleanEvoBPM.WebAPI.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanEvoBPM.WebAPI.Controllers
{
    [Authorize(Roles = UserRole.Admin)]
    public class BusinessDomainsController : CustomBaseApiController
    {
        private readonly IUserHelper _userHelper;
        private readonly string userLogin;
        public BusinessDomainsController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
            userLogin = _userHelper != null ? _userHelper.GetUserName() : "";
        }

        [HttpGet]
        public async Task<IEnumerable<BusinessDomainResponseModel>> Get([FromQuery] FetchBusinessDomainQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessDomainResponseModel>> Get(Guid id)
        {
            var query = new GetBusinessDomainDetailsQuery() { Id = id };
            return await Mediator.Send(query);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<BusinessDomainResponseModel>> Get(string name)
        {
            name = !string.IsNullOrEmpty(name) ? name.Trim() : string.Empty;
            var query = new GetBusinessDomainDetailsQuery() { Name = name };
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Post(CreateBusinessDomainCommand command)
        {
            command.CreatedBy = userLogin;
            var result = await Mediator.Send(command);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] UpdateBusinessDomainCommand request)
        {
            request.Id = id;
            request.UpdatedBy = userLogin;
            return await Mediator.Send(request);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponse>> Delete(Guid id)
        {
            var query = new DeleteBusinessDomainCommand() { Id = id, UpdatedBy = userLogin };
            return await Mediator.Send(query);
        }
    }
}

