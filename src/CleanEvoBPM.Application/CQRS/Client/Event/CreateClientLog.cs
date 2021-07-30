using CleanEvoBPM.Application.CQRS.Client.Command;
using CleanEvoBPM.Application.Models.Client;
using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Client.Event
{
    public class CreateClientLog : IDomainEvent
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string ClientDivisionName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public CreateClientLog(CreateClientCommand model)
        {
            this.Id                 = model.Id;
            this.ClientName         = model.ClientName;
            this.ClientDivisionName = model.ClientDivisionName;
            this.CreatedDate        = model.CreatedDate;
            this.UpdatedDate        = model.UpdatedDate;
        }

        public CreateClientLog(ClientResponseModel model)
        {
            this.Id = model.Id;
            this.ClientName = model.ClientName;
            this.ClientDivisionName = model.ClientDivisionName;
            this.CreatedDate = model.CreatedDate;
            this.UpdatedDate = model.UpdatedDate;
        }
    }
}
