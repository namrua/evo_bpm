using System;
using System.Collections.Generic;
using System.Text;
using CleanEvoBPM.Application.Common;
using MediatR;
namespace CleanEvoBPM.Application.CQRS.Status.Command
{
    public class DeleteStatusCommand : IRequest<GenericResponse>
    {
         public Guid Id { get; set; }
    }
}