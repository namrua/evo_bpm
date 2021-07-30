using System;
using CleanEvoBPM.Domain.Enums;

namespace CleanEvoBPM.Application.Models.Project
{
    public class ProjectResponseModel
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public Guid ProjectManagerId { get; set; }
        public Guid ProjectTypeId { get; set; }
        public Guid ServiceTypeId { get; set; }
        public Guid BusinessDomainId { get; set; }
        public string BusinessDomainDisplay { get; set; }
        public Guid MethodologyId { get; set; }
        public Guid TechnologyId { get; set; }
        public Guid StatusId { get; set; }
        public Guid DeliveryODCId { get; set; }
        public Guid DeliveryLocationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
