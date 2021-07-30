using CleanEvoBPM.Application.CQRS.Project.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Project;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Project.QueryHandler
{
    public class GetProjectsByManagerQueryHandler : BaseProjectHandler, IRequestHandler<GetProjectsByManagerQuery, IEnumerable<ProjectResponseModel>>
    {
        public GetProjectsByManagerQueryHandler(IProjectDataService projectDataService,
            IBusinessDomainDataService businessDomainDataService,
            IClientDataService clientDataService)
            : base(projectDataService,
                  businessDomainDataService,
                  clientDataService)
        {
        }

        public async Task<IEnumerable<ProjectResponseModel>> Handle(GetProjectsByManagerQuery request, CancellationToken cancellationToken)
        {
            var result = await _projectDataService.GetProjectsByManagerId(request.ManagerId);
            return result;
        }
    }
}
