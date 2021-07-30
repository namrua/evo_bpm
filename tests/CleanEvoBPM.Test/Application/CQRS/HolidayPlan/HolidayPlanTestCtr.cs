using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.CommandHandler;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.QueryHandler;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Domain;
using Moq;

namespace CleanEvoBPM.Test.Application.CQRS.ProjectHolidayPlan
{
    public partial class ProjectHolidayPlanTest
    {
        public Mock<IProjectHolidayPlanDataService> _projectHolidayPlanDataServiceMock;
        public GetProjectHolidayPlanQueryHandler _getProjectHolidayPlanQueryHandler;
        public CreateProjectHolidayPlanCommandHandler _createProjectHolidayPlanCommandHandler;
        public UpdateProjectHolidayPlanCommandHandler _updateProjectHolidayPlanCommandHandler;
        public DeleteProjectHolidayPlanCommandHandler _deleteProjectHolidayPlanCommandHandler;
        private readonly Mock<INotificationDispatcher> _mockNotificationDispatcher;

        public ProjectHolidayPlanTest()
        {
            _projectHolidayPlanDataServiceMock = new Mock<IProjectHolidayPlanDataService>();
            _mockNotificationDispatcher = new Mock<INotificationDispatcher>();
            _getProjectHolidayPlanQueryHandler = new GetProjectHolidayPlanQueryHandler(_projectHolidayPlanDataServiceMock.Object,
                _mockNotificationDispatcher.Object);
            _createProjectHolidayPlanCommandHandler = new CreateProjectHolidayPlanCommandHandler(_projectHolidayPlanDataServiceMock.Object,
                _mockNotificationDispatcher.Object);
            _updateProjectHolidayPlanCommandHandler = new UpdateProjectHolidayPlanCommandHandler(_projectHolidayPlanDataServiceMock.Object,
                _mockNotificationDispatcher.Object);
            _deleteProjectHolidayPlanCommandHandler = new DeleteProjectHolidayPlanCommandHandler(_projectHolidayPlanDataServiceMock.Object,
                _mockNotificationDispatcher.Object);
        }
    }
}
