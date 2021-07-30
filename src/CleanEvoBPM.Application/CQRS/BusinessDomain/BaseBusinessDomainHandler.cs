using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectBusinessDomain;

namespace CleanEvoBPM.Application.CQRS.BusinessDomain
{
    public class BaseBusinessDomainHandler
    {
        public readonly IBusinessDomainDataService _businessDomainDataService;

        public BaseBusinessDomainHandler(IBusinessDomainDataService businessDomainDataService)
        {
            _businessDomainDataService = businessDomainDataService;
        }
    }
}