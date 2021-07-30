using System;
using CleanEvoBPM.Domain.Enums;

namespace CleanEvoBPM.Application.Models.Methodology
{
    public class MethodologyResponseModel
    {
        public Guid Id { get; set; }
        public string MethodologyName { get; set; }
        public string Description { get; set; }
        public bool RecordStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
