using CleanEvoBPM.Application.CQRS.Methodology.Command;
using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Methodology.Event
{
    public class DeleteMethodologyLog : IDomainEvent
    {
        public Guid Id { get; set; }

        public DeleteMethodologyLog(Guid requestId)
        {
            this.Id = requestId;
        }
    }
}
