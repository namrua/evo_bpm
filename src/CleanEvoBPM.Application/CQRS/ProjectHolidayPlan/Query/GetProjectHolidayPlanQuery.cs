using CleanEvoBPM.Application.Models.ProjectHolidayPlan;
using MediatR;
using System;
using System.Collections.Generic;

namespace CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Query
{
    public class GetProjectHolidayPlanQuery : IRequest<IEnumerable<ProjectHolidayPlanResponseModel>>
    {
        public Guid ProjectId { get; set; }
    }
}
