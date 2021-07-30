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
    public class DeleteProjectHolidayPlanCommandHandler : ProjectHolidayPlanBaseHandler,
        IRequestHandler<DeleteProjectHolidayPlanCommand, GenericResponse>
    {
        public DeleteProjectHolidayPlanCommandHandler(IProjectHolidayPlanDataService projectHolidayPlanDataService, INotificationDispatcher dispatcher)
            : base(projectHolidayPlanDataService, dispatcher)
        {
        }

        public async Task<GenericResponse> Handle(DeleteProjectHolidayPlanCommand request, CancellationToken cancellationToken)
        {
            var result = await _projectHolidayPlanDataService.Delete(request.Id);

            if (result.Success)
            {
                _ = _dispatcher.Push(new DeleteProjectHolidayPlanLog(request.Id));
            }

            return result;
        }
    }
}
