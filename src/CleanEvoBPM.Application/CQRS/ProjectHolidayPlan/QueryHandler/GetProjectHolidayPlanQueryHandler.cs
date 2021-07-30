using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectHolidayPlan;
using CleanEvoBPM.Domain;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.QueryHandler
{
    public class GetProjectHolidayPlanQueryHandler : ProjectHolidayPlanBaseHandler,
        IRequestHandler<GetProjectHolidayPlanQuery, IEnumerable<ProjectHolidayPlanResponseModel>>
    {
        public GetProjectHolidayPlanQueryHandler(IProjectHolidayPlanDataService projectHolidayPlanDataService, INotificationDispatcher dispatcher)
            : base(projectHolidayPlanDataService, dispatcher)
        {
        }
        public async Task<IEnumerable<ProjectHolidayPlanResponseModel>> Handle(GetProjectHolidayPlanQuery request, CancellationToken cancellationToken)
        {
            var result = await _projectHolidayPlanDataService.Get(request);

            return result;
        }
    }
}
