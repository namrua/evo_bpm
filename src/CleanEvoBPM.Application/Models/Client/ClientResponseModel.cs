using System;

namespace CleanEvoBPM.Application.Models.Client
{
    public class ClientResponseModel
    {
        public Guid Id {get; set;}
        public string ClientName { get; set; }
        public string ClientDivisionName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}