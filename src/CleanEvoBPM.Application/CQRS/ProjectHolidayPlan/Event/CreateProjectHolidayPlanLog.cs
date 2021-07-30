using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Command;
using CleanEvoBPM.Domain;
using System;

namespace CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Event
{
    public class CreateProjectHolidayPlanLog : IDomainEvent
    {
        public CreateProjectHolidayPlanLog(CreateProjectHolidayPlanCommand command)
        {
            Id = command.Id;
            ProjectId = command.ProjectId;
            ResourceId = command.ResourceId;
            ResourceRoleId = command.ResourceRoleId;
            FromDate = command.FromDate;
            ToDate = command.ToDate;
            Note = command.Note;
            CreatedDate = command.CreatedDate;
            UpdatedDate = command.UpdatedDate;
            CreatedBy = command.CreatedBy;
            LastUpdatedBy = command.LastUpdatedBy;
        }

        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ResourceId { get; set; }
        public Guid ResourceRoleId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid LastUpdatedBy { get; set; }
    }
}
