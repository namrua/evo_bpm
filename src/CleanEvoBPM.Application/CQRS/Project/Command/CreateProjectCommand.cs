using System;
using System.Collections.Generic;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.Models.Client;
using CleanEvoBPM.Application.Models.ProjectLLBP;
using CleanEvoBPM.Application.Models.ProjectMilestone;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.Project.Command
{
    public class CreateProjectCommand : IRequest<GenericResponse>
    {
     public Guid Id { get; set; }
        public Client Client { get; set; }        
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public Guid ProjectManagerId { get; set; }
        public Guid ProjectTypeId { get; set; }
        public Guid ServiceTypeId { get; set; }
        public List<Guid> BusinessDomainId { get; set; }
        public List<Guid> MethodologyId { get; set; }
        public List<Guid> TechnologyId { get; set; }
        public Guid? StatusId { get; set; }
        public Guid DeliveryODCId { get; set; }
        public Guid DeliveryLocationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public IEnumerable<string> Risks { get; set; }
        public IEnumerable<ProjectMilestoneModel> Milestones { get; set; }
        public IEnumerable<ProjectLLBPModel> LessonLearntBestPractives { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
    public class Client
    {
        public string ClientName { get; set; }
        public string ClientDivisionName { get; set; }
    }


    public class CreateProjectCommand2 : IRequest<GenericResponse>
    {
        public Guid Id { get; set; }        
        public Guid ClientId { get; set; }
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
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
