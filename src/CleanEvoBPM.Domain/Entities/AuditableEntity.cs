using System;
namespace CleanEvoBPM.Domain.Entities
{
    public class AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UpdatedUser { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
