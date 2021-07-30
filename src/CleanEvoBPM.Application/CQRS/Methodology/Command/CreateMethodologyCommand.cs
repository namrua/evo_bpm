using CleanEvoBPM.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Methodology.Command
{
    public class CreateMethodologyCommand : IRequest<GenericResponse>
    {
        public Guid Id { get; set; }
        public string MethodologyName { get; set; }
        public string Description { get; set; }
        public bool RecordStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
