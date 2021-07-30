using CleanEvoBPM.Application.DatabaseServices.Interfaces;
namespace CleanEvoBPM.Application.CQRS.ProjectMilestone
{
    public class BaseProjectMilestoneHandle
    {
        public readonly IProjectMilestoneDataService _projectMilestoneDataService;
        public BaseProjectMilestoneHandle(IProjectMilestoneDataService projectMilestoneDataService)
        {
            _projectMilestoneDataService = projectMilestoneDataService;
        }
    }
}
