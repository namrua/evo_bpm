using CleanEvoBPM.Application.Common;
using MediatR;
using System;

namespace CleanEvoBPM.Application.CQRS.BusinessDomain.Command
{
    public class CreateBusinessDomainCommand : IRequest<GenericResponse>
    {
        public Guid Id { get; set; }
        public string BusinessDomainName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}