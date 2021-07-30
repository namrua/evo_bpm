using CleanEvoBPM.Application.CQRS.ServiceType.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ServiceType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.ServiceType.QueryHandler
{
    public class GetServiceTypeQueryHandler : BaseServiceTypeHandler, IRequestHandler<GetServiceTypeQuery, IEnumerable<ServiceTypeResponseModel>>
    {
        public GetServiceTypeQueryHandler(IServiceTypeDataService productDataService) : base(productDataService)
        {
        }

        public async Task<IEnumerable<ServiceTypeResponseModel>> Handle(GetServiceTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await _serviceTypeDataService.FetchServiceType(request);
            return result;
        }
    }
}

