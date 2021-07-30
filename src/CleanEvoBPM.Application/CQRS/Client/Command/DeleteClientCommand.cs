using CleanEvoBPM.Application.Common;
using MediatR;
using System;

namespace CleanEvoBPM.Application.CQRS.Client.Command
{
    public class DeleteClientCommand : IRequest<GenericResponse>
    {
        public Guid Id { get; set; }
        public string UpdatedBy { get; set; }
    }
}