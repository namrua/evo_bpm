using CleanEvoBPM.Application.Common;
using MediatR;
using System;

namespace CleanEvoBPM.Application.CQRS.Client.Command
{
    public class UpdateClientCommand : IRequest<GenericResponse>
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string ClientDivisionName { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}