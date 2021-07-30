using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Domain;

namespace CleanEvoBPM.Application.CQRS.ProjectHolidayPlan
{
    public class ProjectHolidayPlanBaseHandler
    {
        protected readonly IProjectHolidayPlanDataService _projectHolidayPlanDataService;
        protected readonly INotificationDispatcher _dispatcher;
        protected ProjectHolidayPlanBaseHandler(IProjectHolidayPlanDataService projectHolidayPlanDataService, INotificationDispatcher dispatcher)
        {
            _projectHolidayPlanDataService = projectHolidayPlanDataService;
            _dispatcher = dispatcher;
        }
    }
}
