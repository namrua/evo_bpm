using CleanEvoBPM.Application.CQRS.Client.Command;
using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Client.Event
{
    public class UpdateClientLog : IDomainEvent
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string ClientDivisionName { get; set; }
        public DateTime UpdatedDate { get; set; }

        public UpdateClientLog(UpdateClientCommand model)
        {
            this.Id = model.Id;
            this.ClientName = model.ClientName;
            this.ClientDivisionName = model.ClientDivisionName;            
            this.UpdatedDate = model.UpdatedDate;
        }
    }
}
