using CleanEvoBPM.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Status.Command
{
    public class CreateStatusCommand : IRequest<GenericResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }        
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
