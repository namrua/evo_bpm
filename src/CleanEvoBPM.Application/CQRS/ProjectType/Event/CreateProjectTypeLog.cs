using CleanEvoBPM.Application.CQRS.ProjectType.Command;
using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.ProjectType.Event
{
    public class CreateProjectTypeLog : IDomainEvent
    {
        public Guid Id { get; set; }
        public string ProjectTypeName { get; set; }
        public bool RecordStatus { get; set; }
        public string ProjectTypeDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public CreateProjectTypeLog(CreateProjectTypeCommand model)
        {
            this.Id                     = model.Id;
            this.ProjectTypeName        = model.ProjectTypeName;
            this.ProjectTypeDescription = model.ProjectTypeDescription;
            this.RecordStatus           = model.RecordStatus;
            this.CreatedDate            = model.CreatedDate;
            this.UpdatedDate            = model.UpdatedDate;
        }
    }
}
