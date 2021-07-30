using CleanEvoBPM.Application.Models.ProjectType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.ProjectType.Query
{
    public class GetProjectTypeQuery : IRequest<IEnumerable<ProjectTypeResponseModel>>
    {
        public bool? RecordStatus { get; set; }        
        public Guid? Id { get; set; }
        public string ProjectTypeName { get; set; }
    }
}
