using System;

namespace CleanEvoBPM.Application.Models.Technology
{
    public class TechnologyResponseModel
    {
        public Guid Id { get; set; }
        public string TechnologyName { get; set; }
        public string TechnologyDescription { get; set; }
        public bool TechnologyActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
    }
}
