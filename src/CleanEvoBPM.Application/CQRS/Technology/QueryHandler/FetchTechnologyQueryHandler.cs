using CleanEvoBPM.Application.CQRS.Technology.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Technology;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Technology.QueryHandler
{
    public class FetchTechnologyQueryHandler : BaseTechnologyHandler, IRequestHandler<FetchTechnologyQuery, IEnumerable<TechnologyResponseModel>>
    {
        public FetchTechnologyQueryHandler(ITechnologyDataService technologyDataService) : base(technologyDataService)
        {
        }
        public async Task<IEnumerable<TechnologyResponseModel>> Handle(FetchTechnologyQuery request, CancellationToken cancellationToken)
        {
            var result = await _technologyDataService.Fetch(request);
            return result;
        }
    }
}
