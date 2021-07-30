using CleanEvoBPM.Application.CQRS.ProjectType.Command;
using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.ProjectType.Event
{
    public class DeleteProjectTypeLog : IDomainEvent
    {
        public Guid Id { get; set; }
        public DeleteProjectTypeLog(Guid RequestId)
        {
            this.Id = RequestId;
        }
    }
}
