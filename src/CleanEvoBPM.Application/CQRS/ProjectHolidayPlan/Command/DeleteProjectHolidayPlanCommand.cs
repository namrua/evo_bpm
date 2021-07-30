using CleanEvoBPM.Application.Common;
using MediatR;
using System;

namespace CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Command
{
    public class DeleteProjectHolidayPlanCommand : IRequest<GenericResponse>
    {
        public Guid Id { get; set; }
    }
}