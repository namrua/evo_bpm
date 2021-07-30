using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.DeliveryLocation.Query;
using CleanEvoBPM.Application.Models.DeliveryLocation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanEvoBPM.WebAPI.Controllers
{
    //[Authorize(Roles = UserRole.Admin)]
    public class DeliveryLocationController : CustomBaseApiController
    {
        [HttpGet]
        public async Task<IEnumerable<DeliveryLocationResponseModel>> Get([FromQuery]GetDeliveryLocationQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
