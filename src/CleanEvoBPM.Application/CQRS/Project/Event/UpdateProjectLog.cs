using CleanEvoBPM.Application.CQRS.Project.Command;
using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Project.Event
{
    public class UpdateProjectLog : IDomainEvent
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

        public UpdateProjectLog(UpdateProjectCommand model)
        {
            this.Id               = model.Id;            
            this.ProjectName      = model.ProjectName;
            this.ProjectCode      = model.ProjectCode;
            this.ProjectManagerId = model.ProjectManagerId;
            this.ProjectTypeId    = model.ProjectTypeId;
            this.ServiceTypeId    = model.ServiceTypeId;
            this.BusinessDomainId = model.BusinessDomainId;
            this.MethodologyId    = model.MethodologyId;
            this.TechnologyId     = model.TechnologyId;
            this.StatusId         = model.StatusId;
            this.DeliveryODCId = model.DeliveryODCId;
            this.DeliveryLocationId = model.DeliveryLocationId;
            this.StartDate        = model.StartDate;
            this.LastUpdated      = model.LastUpdated;
        }
    }
}
