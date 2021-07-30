using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Client.Event
{
    public class DeleteClientLog : IDomainEvent
    {
        public Guid Id { get; set; }

        public DeleteClientLog(Guid requestId)
        {
            this.Id = requestId;
        }
    }
}
