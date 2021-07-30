using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.DeliveryODC.Query;
using CleanEvoBPM.Application.Models.DeliveryODC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanEvoBPM.WebAPI.Controllers
{
    //[Authorize(Roles = UserRole.Admin)]
    public class DeliveryODCController : CustomBaseApiController
    {
        [HttpGet]
        public async Task<IEnumerable<DeliveryODCResponseModel>> Get([FromQuery]GetDeliveryODCQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
