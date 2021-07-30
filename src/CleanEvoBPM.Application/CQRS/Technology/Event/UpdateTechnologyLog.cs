using CleanEvoBPM.Domain;
using System;

namespace CleanEvoBPM.Application.CQRS.Technology.Event
{
    public class UpdateTechnologyLog : IDomainEvent
    {
        public Guid Id { get; set; }
        public string TechnologyName { get; set; }
        public string TechnologyDescription { get; set; }
        public bool TechnologyActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }

        public UpdateTechnologyLog(TechnologyCommit model)
        {
            this.Id = model.Id;
            this.TechnologyName = model.TechnologyName;
            this.TechnologyDescription = model.TechnologyDescription;
            this.TechnologyActive = model.TechnologyActive;
            this.CreatedDate = model.CreatedDate;
            this.UpdatedDate = model.UpdatedDate;
            this.UpdatedUser = model.UpdatedUser;
        }
    }
}
