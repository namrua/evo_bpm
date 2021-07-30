using CleanEvoBPM.Application.CQRS.Project.Command;
using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Project.Event
{
    public class CreateProjectLog : IDomainEvent
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
        public DateTime CreatedUpdated { get; set; }
        public DateTime CreatedDate { get; set; }        

        public CreateProjectLog(CreateProjectCommand2 model, Guid clientID)
        {
            this.Id               = model.Id;
            this.ClientId         = clientID;
            this.ProjectName      = model.ProjectName;
            this.ProjectCode      = model.ProjectCode;
            this.ProjectManagerId = model.ProjectManagerId;
            this.ProjectTypeId    = model.ProjectTypeId;
            this.ServiceTypeId    = model.ServiceTypeId;
            this.BusinessDomainId = model.BusinessDomainId;
            this.MethodologyId    = model.MethodologyId;
            this.TechnologyId     = model.TechnologyId;
            this.StatusId         = model.StatusId;
            this.DeliveryODCId      = model.DeliveryODCId;
            this.DeliveryLocationId = model.DeliveryLocationId;
            this.StartDate        = model.StartDate;
            this.LastUpdated      = model.LastUpdated;
            this.CreatedUpdated   = model.CreatedDate;
            this.CreatedDate      = model.CreatedDate;            
        }
    }
}
