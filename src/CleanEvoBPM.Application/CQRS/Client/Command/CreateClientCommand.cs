using CleanEvoBPM.Application.Common;
using MediatR;
using System;

namespace CleanEvoBPM.Application.CQRS.Client.Command
{
    public class CreateClientCommand : IRequest<GenericResponse>
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string ClientDivisionName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}