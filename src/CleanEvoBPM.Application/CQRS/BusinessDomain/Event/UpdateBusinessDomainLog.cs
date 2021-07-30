using CleanEvoBPM.Application.CQRS.BusinessDomain.Command;
using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.BusinessDomain.Event
{
    public class UpdateBusinessDomainLog : IDomainEvent
    {
        public Guid Id { get; set; }
        public string BusinessDomainName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public UpdateBusinessDomainLog(UpdateBusinessDomainCommand model)
        {
            this.Id = model.Id;
            this.BusinessDomainName = model.BusinessDomainName;
            this.Description = model.Description;
            this.Status = model.Status;
        }
    }
}
