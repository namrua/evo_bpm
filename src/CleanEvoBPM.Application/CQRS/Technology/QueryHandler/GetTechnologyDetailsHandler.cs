using CleanEvoBPM.Application.CQRS.Technology.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Technology;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Technology.QueryHandler
{
    public class GetTechnologyDetailsHandler : IRequestHandler<GetTechnologyDetailsQuery, TechnologyResponseModel>
    {
        private readonly ITechnologyDataService _technologyDataService;
        public GetTechnologyDetailsHandler(ITechnologyDataService technologyDataService)
        {
            _technologyDataService = technologyDataService;
        }

        public async Task<TechnologyResponseModel> Handle(GetTechnologyDetailsQuery request, CancellationToken cancellationToken)
        {
            var result = await _technologyDataService.GetTechnologyDetail(request);
            return result;
        }
    }
}
