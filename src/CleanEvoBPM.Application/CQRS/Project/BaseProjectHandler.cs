using CleanEvoBPM.Application.DatabaseServices.Interfaces;

namespace CleanEvoBPM.Application.CQRS.Project
{
    public class BaseProjectHandler
    {
        public readonly IProjectDataService _projectDataService;
        public readonly IBusinessDomainDataService _businessDomainDataService;
        public readonly IClientDataService _clientDataService;
        public BaseProjectHandler(IProjectDataService projectDataService,
            IBusinessDomainDataService businessDomainDataService,
            IClientDataService clientDataService)
        {
            _projectDataService = projectDataService;
            _businessDomainDataService = businessDomainDataService;
            _clientDataService = clientDataService;
        }
    }
}
