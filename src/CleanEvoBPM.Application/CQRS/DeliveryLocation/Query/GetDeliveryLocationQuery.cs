using CleanEvoBPM.Application.Models.DeliveryLocation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.DeliveryLocation.Query
{
    public class GetDeliveryLocationQuery : IRequest<IEnumerable<DeliveryLocationResponseModel>>
    {
        public Guid Id { get; set; }
        public bool? Status { get; set; }
    }
}
