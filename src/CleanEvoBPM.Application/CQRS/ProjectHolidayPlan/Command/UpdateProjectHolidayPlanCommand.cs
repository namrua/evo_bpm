using CleanEvoBPM.Application.Common;
using MediatR;
using System;

namespace CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Command
{
    public class UpdateProjectHolidayPlanCommand : IRequest<GenericResponse>
    {
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
