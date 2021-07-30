using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.BusinessDomain;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.BusinessDomain.QueryHandler {
    public class GetBusinessDomainDetailsHandler : IRequestHandler<GetBusinessDomainDetailsQuery, BusinessDomainResponseModel> {
        private readonly IBusinessDomainDataService _businessDomainDataService;
        public GetBusinessDomainDetailsHandler (IBusinessDomainDataService businessDomainDataService) {
            _businessDomainDataService = businessDomainDataService;
        }

        public async Task<BusinessDomainResponseModel> Handle (GetBusinessDomainDetailsQuery request, CancellationToken cancellationToken) {
            var result = await _businessDomainDataService.GetBusinessDomainDetail(request);
            return result;
        }
    }
}