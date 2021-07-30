using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Models.DeliveryODC
{
    public class DeliveryODCResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdated { get; set; }
        public Guid UpdatedBy { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
