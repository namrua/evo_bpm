using CleanEvoBPM.Application.CQRS.Methodology.Command;
using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Methodology.Event
{
    public class UpdateMethodologyLog : IDomainEvent
    {
        public Guid Id { get; set; }
        public string MethodologyName { get; set; }
        public bool RecordStatus { get; set; }

        public UpdateMethodologyLog(UpdateMethodologyCommand model)
        {
            this.Id = model.Id;
            this.MethodologyName = model.MethodologyName;
            this.RecordStatus = model.RecordStatus;
        }
    }
}
