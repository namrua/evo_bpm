using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.BusinessDomain;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.BusinessDomain.QueryHandler
{
    public class FetchBusinessDomainQueryHandler : BaseBusinessDomainHandler,
        IRequestHandler<FetchBusinessDomainQuery, IEnumerable<BusinessDomainResponseModel>>
    {
        public FetchBusinessDomainQueryHandler(IBusinessDomainDataService businessDomainDataService) : base(businessDomainDataService)
        {
        }

        public async Task<IEnumerable<BusinessDomainResponseModel>> Handle(FetchBusinessDomainQuery request, CancellationToken cancellationToken)
        {
            var result = await _businessDomainDataService.FetchBusinessDomain(request);
            return result;
        }
    }
}