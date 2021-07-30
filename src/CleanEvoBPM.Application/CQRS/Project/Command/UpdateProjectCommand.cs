using System;
using System.Collections.Generic;
using System.Text;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.Models.ProjectLLBP;
using CleanEvoBPM.Application.Models.ProjectMilestone;
using CleanEvoBPM.Application.Models.ProjectRisk;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.Project.Command
{
    public class UpdateProjectCommand : IRequest<GenericResponse>
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public Guid ProjectManagerId { get; set; }
        public Guid ProjectTypeId { get; set; }
        public Guid ServiceTypeId { get; set; }
        public List<Guid> BusinessDomainId { get; set; }
        public List<Guid> MethodologyId { get; set; }
        public List<Guid> TechnologyId { get; set; }
        public Guid? StatusId { get; set; }
        public Guid? DeliveryODCId { get; set; }
        public Guid? DeliveryLocationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public IEnumerable<ProjectMilestoneModel> Milestones { get; set; }
        public IEnumerable<string> Risks { get; set; }
        public IEnumerable<ProjectLLBPModel> LessonLearntBestPractives { get; set; }
    }
}
