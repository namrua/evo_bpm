using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Command;
using CleanEvoBPM.Application.Models.ProjectHolidayPlan;
using CleanEvoBPM.Domain;
using System;

namespace CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Event
{
    public class UpdateProjectHolidayPlanLog : IDomainEvent
    {
        public UpdateProjectHolidayPlanLog(UpdateProjectHolidayPlanCommand model)
        {
            Id = model.Id;
            ResourceId = model.ResourceId;
            ResourceRoleId = model.ResourceRoleId;
            FromDate = model.FromDate;
            ToDate = model.ToDate;
            Note = model.Note;
            UpdatedDate = model.UpdatedDate;
            LastUpdatedBy = model.LastUpdatedBy;
        }

        public Guid Id { get; set; }
        public Guid ResourceId { get; set; }
        public Guid ResourceRoleId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Note { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid LastUpdatedBy { get; set; }
    }
}
