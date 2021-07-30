using CleanEvoBPM.Application.CQRS.Methodology.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Methodology;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Methodology.QueryHandler
{    
    public class GetMethodologyQueryHandler : BaseMethodologyHandler, IRequestHandler<GetMethodologyQuery, IEnumerable<MethodologyResponseModel>>
    {
        public GetMethodologyQueryHandler(IMethodologyDataService methodologyDataService) : base(methodologyDataService)
        {
        }
        public async Task<IEnumerable<MethodologyResponseModel>> Handle(GetMethodologyQuery request, CancellationToken cancellationToken)
        {
            var result = await _methodologyDataService.FetchMethodology(request);

            return result;
        }
    }
}
