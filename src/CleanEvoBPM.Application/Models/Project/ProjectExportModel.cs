using System;

namespace CleanEvoBPM.Application.Models.Project
{
    public class ProjectExportModel
    {
        // public Guid Id { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }


        public string ProjectTypeName { get; set; }
        public string ServiceTypeName { get; set; }
        public string MethodologyName { get; set; }
        public string BusinessDomainDisplay { get; set; }
        public string CreatedDate { get; set; }

        public string LastUpdated { get; set; }
        public string Name { get; set; }

    }
}