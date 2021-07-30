using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Project.Event
{
    public class DeleteProjectLog : IDomainEvent
    {
        public Guid Id { get; set; }

        public DeleteProjectLog(Guid projectId)
        {
            this.Id = projectId;
        }
    }
}
