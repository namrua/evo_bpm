using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.Models.BusinessDomain;
using CleanEvoBPM.Application.Models.Client;
using CleanEvoBPM.Application.Models.DeliveryLocation;
using CleanEvoBPM.Application.Models.DeliveryODC;
using CleanEvoBPM.Application.Models.Methodology;
using CleanEvoBPM.Application.Models.ProjectType;
using CleanEvoBPM.Application.Models.ServiceType;
using CleanEvoBPM.Application.Models.Status;
using CleanEvoBPM.Application.Models.Technology;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Models.Project
{
    public class ProjectResponseByIdModel
    {
        public Guid Id { get; set; }
        public ClientResponseModel Client { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public Guid ProjectManagerId { get; set; }
        public ProjectTypeResponseModel ProjectType { get; set; }
        public ServiceTypeResponseModel ServiceType { get; set; }
        public List<BusinessDomainResponseModel> BusinessDomains { get; set; }
        public List<MethodologyResponseModel> Methodologies { get; set; }
        public List<TechnologyResponseModel> Technologies { get; set; }
        public StatusResponseModel Status { get; set; }
        public DeliveryODCResponseModel DeliveryODC { get; set; }
        public DeliveryLocationResponseModel DeliveryLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
