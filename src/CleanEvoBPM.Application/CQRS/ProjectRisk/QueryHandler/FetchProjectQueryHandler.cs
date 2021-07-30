using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.ProjectRisk.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectRisk;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.ProjectRisk.QueryHandler
{
    public class FetchProjectQueryHandler : BaseProjectRiskHandler, IRequestHandler<FetchProjectRiskQuery, IEnumerable<ProjectRiskModel>>
    {
        public FetchProjectQueryHandler(IProjectRiskDataService projectRiskdataService) : base(projectRiskdataService)
        {
        }

        public async Task<IEnumerable<ProjectRiskModel>> Handle(FetchProjectRiskQuery request, CancellationToken cancellationToken)
        {
            var result = await _projectRiskdataService.GetProjectRisks(request.ProjectId);
            return result;
        }
    }
}