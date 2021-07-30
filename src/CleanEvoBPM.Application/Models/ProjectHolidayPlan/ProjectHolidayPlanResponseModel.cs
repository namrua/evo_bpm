using System;

namespace CleanEvoBPM.Application.Models.ProjectHolidayPlan
{
    public class ProjectHolidayPlanResponseModel
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ResourceId { get; set; }
        public string Resource { get; set; }
        public Guid ResourceRoleId { get; set; }
        public string ResourceRole { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid LastUpdatedBy { get; set; }
    }
}
