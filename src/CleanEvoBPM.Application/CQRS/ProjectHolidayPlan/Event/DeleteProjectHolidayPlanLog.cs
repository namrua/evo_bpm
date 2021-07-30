using CleanEvoBPM.Domain;
using System;

namespace CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Event
{
    public class DeleteProjectHolidayPlanLog : IDomainEvent
    {
        public DeleteProjectHolidayPlanLog(Guid requestId)
        {
            Id = requestId;
        }

        public Guid Id { get; set; }
    }
}
