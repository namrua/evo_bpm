using CleanEvoBPM.Application.Common;
using MediatR;
using System;

namespace CleanEvoBPM.Application.CQRS.Technology.Command
{
    public class DeleteTechnologyCommand : IRequest<GenericResponse>
    {
        public Guid Id { get; set; }
        public string UpdatedBy { get; set; }
    }
}
