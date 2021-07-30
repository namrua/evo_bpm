using System;
using CleanEvoBPM.Application.Common;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.BusinessDomain.Command
{
    public class DeleteBusinessDomainCommand : IRequest<GenericResponse>
    {
        public Guid Id { get; set; }
        public string UpdatedBy { get; set; }
    }
}