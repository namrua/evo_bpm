using CleanEvoBPM.Application.DatabaseServices.Interfaces;

namespace CleanEvoBPM.Application.CQRS.ProjectRisk
{
    public class BaseProjectRiskHandler
    {
        public readonly IProjectRiskDataService _projectRiskdataService;
        public BaseProjectRiskHandler(IProjectRiskDataService projectRiskdataService)
        {
            _projectRiskdataService = projectRiskdataService;
        }
    }
}