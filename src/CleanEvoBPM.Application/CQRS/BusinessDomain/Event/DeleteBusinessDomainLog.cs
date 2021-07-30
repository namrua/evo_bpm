using CleanEvoBPM.Application.CQRS.BusinessDomain.Command;
using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.BusinessDomain.Event
{
    public class DeleteBusinessDomainLog : IDomainEvent
    {
        public Guid Id { get; set; }
        public DeleteBusinessDomainLog(Guid requestID)
        {
            this.Id = requestID;
        }
    }
}
