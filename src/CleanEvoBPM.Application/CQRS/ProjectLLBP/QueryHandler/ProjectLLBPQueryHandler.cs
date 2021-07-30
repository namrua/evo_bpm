using CleanEvoBPM.Application.CQRS.ProjectLLBP.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectLLBP;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.ProjectLLBP.QueryHandler
{
    public class ProjectLLBPQueryHandler : BaseProjectLLBPHandle, IRequestHandler<ProjectLLBPQuery, IEnumerable<ProjectLLBPResponseModel>>
    {
        public ProjectLLBPQueryHandler(IProjectLLBPDataService projectLLBPDataService) 
            : base(projectLLBPDataService){}
        public async Task<IEnumerable<ProjectLLBPResponseModel>> Handle(ProjectLLBPQuery request, CancellationToken cancellationToken)
        {
            return await _projectLLBPDataService.FetchProjectLLBP(request.ProjectId);
        }
    }
}
