using System;

namespace CleanEvoBPM.Application.CQRS.Technology.Event
{
    public class TechnologyCommit
    {
        public Guid Id { get; set; }
        public string TechnologyName { get; set; }
        public string TechnologyDescription { get; set; }
        public bool TechnologyActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
