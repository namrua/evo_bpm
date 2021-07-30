using CleanEvoBPM.Application.Models.DeliveryODC;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.DeliveryODC.Query
{
    public class GetDeliveryODCQuery : IRequest<IEnumerable<DeliveryODCResponseModel>>
    {
        public Guid Id { get; set; }
        public bool? Status { get; set; }
    }
}
