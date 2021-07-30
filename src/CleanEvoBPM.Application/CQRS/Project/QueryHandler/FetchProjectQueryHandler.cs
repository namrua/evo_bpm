using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.Project.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Project;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.Project.QueryHandler
{
    public class FetchProjectQueryHandler : BaseProjectHandler, IRequestHandler<FetchProjectQuery, IEnumerable<ProjectResponseModel>>
    {
        public FetchProjectQueryHandler(IProjectDataService projectDataService,
            IBusinessDomainDataService businessDomainDataService,
            IClientDataService clientDataService) 
            : base(projectDataService,
                  businessDomainDataService,
                  clientDataService)
        {
        }
        public async Task<IEnumerable<ProjectResponseModel>> Handle(FetchProjectQuery request, CancellationToken cancellationToken)
        {
            var result = await _projectDataService.FetchProject(request);
            return result;
        }
    }
}
