using CleanEvoBPM.Application.CQRS.ProjectMilestone.Query;
using CleanEvoBPM.Application.CQRS.Technology;
using CleanEvoBPM.Application.CQRS.Technology.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProblemCategory;
using CleanEvoBPM.Application.Models.ProjectMilestone;
using CleanEvoBPM.Application.Models.Technology;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.ProjectMilestone.QueryHandle
{
    public class FetchProjectMilestoneQueryHandle : BaseProjectMilestoneHandle, IRequestHandler<FetchProjectMilestoneQuery, IEnumerable<ProjectMilestoneModel>>
    {
        public FetchProjectMilestoneQueryHandle(IProjectMilestoneDataService projectMilestoneDataService) : base(projectMilestoneDataService)
        {
        }

        public async Task<IEnumerable<ProjectMilestoneModel>> Handle(FetchProjectMilestoneQuery request, CancellationToken cancellationToken)
        {
            var result = await _projectMilestoneDataService.GetProjectMilestones(request.ProjectId);
            return result;
        }

      
    }
}
