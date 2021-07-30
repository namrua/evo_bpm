using System;
using System.Collections.Generic;
using System.Text;
using CleanEvoBPM.Application.Common;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.ServiceType.Command
{
    public class DeleteServiceTypeCommand : IRequest<GenericResponse>
    {
        public Guid Id { get; set; }
        public string UpdatedBy { get; set; }
    }
}
