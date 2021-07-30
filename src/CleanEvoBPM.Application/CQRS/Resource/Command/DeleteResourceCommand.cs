using CleanEvoBPM.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Resource.Command
{
    public class DeleteResourceCommand:IRequest<GenericResponse>
    {
        public Guid Id { get; set; }
    }
}
