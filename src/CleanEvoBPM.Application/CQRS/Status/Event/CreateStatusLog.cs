using CleanEvoBPM.Application.CQRS.Status.Command;
using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Status.Event
{
    public class CreateStatusLog : IDomainEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }        
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }

        public CreateStatusLog(CreateStatusCommand model)
        {
            this.Id          = model.Id;
            this.Name        = model.Name;            
            this.Active      = model.Active;
            this.CreatedDate = DateTime.UtcNow;
            this.UpdatedDate = model.UpdatedDate;
            //this.UpdatedUser = model.UpdatedUser;
        }
    }
}
