using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Role.Query;
using CleanEvoBPM.Application.Models.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace CleanEvoBPM.WebAPI.Controllers
{
    [Authorize(Roles = UserRole.Admin)]
    [Route("api/[controller]")]
    public class RoleController : CustomBaseApiController
    {
        //GET
        [HttpGet]
        public async Task<IEnumerable<RoleResponseModel>> Get()
        {
            var result =  await Mediator.Send(new FetchRoleQuery());
            return result;
        }
    }
}
