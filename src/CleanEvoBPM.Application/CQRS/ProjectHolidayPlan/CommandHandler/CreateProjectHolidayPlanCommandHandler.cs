using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Command;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Event;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.CommandHandler
{
    public class CreateProjectHolidayPlanCommandHandler : ProjectHolidayPlanBaseHandler,
        IRequestHandler<CreateProjectHolidayPlanCommand, GenericResponse>
    {
        public CreateProjectHolidayPlanCommandHandler(IProjectHolidayPlanDataService projectHolidayPlanDataService, INotificationDispatcher dispatcher)
            : base(projectHolidayPlanDataService, dispatcher)
        {
        }

        public async Task<GenericResponse> Handle(CreateProjectHolidayPlanCommand request, CancellationToken cancellationToken)
        {
            var result = await _projectHolidayPlanDataService.Create(request);
            if (result.Success)
            {
                _ = _dispatcher.Push(new CreateProjectHolidayPlanLog(request));
            }

            return result;
        }
    }
}
