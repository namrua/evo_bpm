using System;
using System.Collections.Generic;
using System.Text;
using CleanEvoBPM.Application.Common;
using MediatR;
namespace CleanEvoBPM.Application.CQRS.ProjectType.Command
{
    public class UpdateProjectTypeCommand : IRequest<GenericResponse>
    {
        public Guid Id { get; set; }
        public string ProjectTypeName { get; set; }
        public bool RecordStatus { get; set; }
        public string ProjectTypeDescription {get;set;}
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
