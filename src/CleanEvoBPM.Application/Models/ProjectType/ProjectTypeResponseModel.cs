using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Models.ProjectType
{
    public class ProjectTypeResponseModel
    {
        public Guid Id { get; set; }
        public string ProjectTypeName { get; set; }
        public bool RecordStatus { get; set; }
        public string ProjectTypeDescription {get;set;}
        public DateTime? UpdatedDate{get;set;}
        public DateTime? CreatedDate{get;set;}
    }
}
