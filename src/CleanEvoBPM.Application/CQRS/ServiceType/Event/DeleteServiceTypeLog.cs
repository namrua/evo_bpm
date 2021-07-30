using CleanEvoBPM.Application.CQRS.ServiceType.Command;
using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.ServiceType.Event
{
    public class DeleteServiceTypeLog : IDomainEvent
    {
        public Guid Id { get; set; }

        public DeleteServiceTypeLog(Guid _id)
        {
            this.Id = _id;        
        }
    }
}
