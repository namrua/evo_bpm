using System;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.BusinessDomain.Command
{
    public class UpdateBusinessDomainCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string BusinessDomainName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}