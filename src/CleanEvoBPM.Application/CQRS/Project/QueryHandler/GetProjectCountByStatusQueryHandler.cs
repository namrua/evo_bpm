using CleanEvoBPM.Application.CQRS.Project.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Project;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Project.QueryHandler
{
    public class GetProjectCountByStatusQueryHandler : BaseProjectHandler, IRequestHandler<GetProjectCountByStatusQuery, IEnumerable<ProjectCountByStatusModel>>
    {
        public GetProjectCountByStatusQueryHandler(IProjectDataService projectDataService,
            IBusinessDomainDataService businessDomainDataService,
            IClientDataService clientDataService)
            : base(projectDataService,
                  businessDomainDataService,
                  clientDataService)
        {
        }

        public async Task<IEnumerable<ProjectCountByStatusModel>> Handle(GetProjectCountByStatusQuery request, CancellationToken cancellationToken)
        {
            return await _projectDataService.GetProjectCountByStatus();
        }
    }
}
