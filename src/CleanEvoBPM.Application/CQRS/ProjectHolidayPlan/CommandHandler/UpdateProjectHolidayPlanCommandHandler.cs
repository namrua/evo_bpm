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
    public class UpdateProjectHolidayPlanCommandHandler : ProjectHolidayPlanBaseHandler,
        IRequestHandler<UpdateProjectHolidayPlanCommand, GenericResponse>
    {
        public UpdateProjectHolidayPlanCommandHandler(IProjectHolidayPlanDataService projectHolidayPlanDataService, INotificationDispatcher dispatcher)
            : base(projectHolidayPlanDataService, dispatcher)
        {
        }

        public async Task<GenericResponse> Handle(UpdateProjectHolidayPlanCommand request, CancellationToken cancellationToken)
        {
            var result = await _projectHolidayPlanDataService.Update(request);

            if (result.Success)
            {
                _ = _dispatcher.Push(new UpdateProjectHolidayPlanLog(request));
            }

            return result;
        }
    }
}
