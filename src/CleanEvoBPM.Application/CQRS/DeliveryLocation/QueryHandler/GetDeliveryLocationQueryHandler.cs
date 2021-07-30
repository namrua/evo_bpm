using CleanEvoBPM.Application.CQRS.DeliveryLocation.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.DeliveryLocation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.DeliveryLocation.QueryHandler
{
    public class GetDeliveryLocationQueryHandler : BaseDeliveryLocationHandler, 
        IRequestHandler<GetDeliveryLocationQuery, IEnumerable<DeliveryLocationResponseModel>>
    {
        public GetDeliveryLocationQueryHandler(IDeliveryLocationDataService deliveryLocationDataService) 
            : base (deliveryLocationDataService)
        {

        }

        public async Task<IEnumerable<DeliveryLocationResponseModel>> Handle(GetDeliveryLocationQuery request, CancellationToken cancellationToken)
        {
            var result = await _deliveryLocationDataService.Fetch(request);
            return result;
        }
    }
}
