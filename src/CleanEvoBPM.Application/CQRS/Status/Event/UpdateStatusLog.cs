using CleanEvoBPM.Application.CQRS.Status.Command;
using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Status.Event
{
    public class UpdateStatusLog : IDomainEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }        
        public bool Active { get; set; }        
        public DateTime UpdatedDate { get; set; }
        //public Guid? UpdatedUser { get; set; }

        //public UpdateStatusLog(StatusCommit model)
        public UpdateStatusLog(UpdateStatusCommand model)
        {
            this.Id          = model.Id;
            this.Name        = model.Name;            
            this.Active      = model.Active;            
            this.UpdatedDate = DateTime.UtcNow;
            //this.UpdatedUser = model.UpdatedUser;
        }
    }
}
