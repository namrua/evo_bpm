using CleanEvoBPM.Application.CQRS.ServiceType.Command;
using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;
namespace CleanEvoBPM.Application.CQRS.Status.Event
{
    public class DeleteStatusLog : IDomainEvent
    {
        public Guid Id { get; set; }

        public DeleteStatusLog(Guid _id)
        {
            this.Id = _id;        
        }
    }
}