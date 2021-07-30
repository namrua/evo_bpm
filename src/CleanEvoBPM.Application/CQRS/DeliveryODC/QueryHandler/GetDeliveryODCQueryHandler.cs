using CleanEvoBPM.Application.CQRS.DeliveryODC.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.DeliveryODC;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.DeliveryODC.QueryHandler
{
    public class GetDeliveryODCQueryHandler : BaseDeliveryODCHandler, IRequestHandler<GetDeliveryODCQuery, IEnumerable<DeliveryODCResponseModel>>
    {
        public GetDeliveryODCQueryHandler(IDeliveryODCDataService deliveryODCDataService) 
            : base (deliveryODCDataService)
        {

        }

        public async Task<IEnumerable<DeliveryODCResponseModel>> Handle(GetDeliveryODCQuery request, CancellationToken cancellationToken)
        {
            var result = await _deliveryODCDataService.Fetch(request);
            return result;
        }
    }
}
