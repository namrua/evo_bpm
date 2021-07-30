using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Models.ServiceType
{
    public class ServiceTypeResponseModel
    {
        #nullable enable
        public Guid Id { get; set; }
        public string? ServiceTypeName { get; set; }
        public string? Description { get; set; }
        public bool RecordStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
