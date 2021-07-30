using System;

namespace CleanEvoBPM.Application.Models.BusinessDomain
{
    public class BusinessDomainResponseModel
    {
        public Guid Id { get; set; }
        public string BusinessDomainName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; }
    }
}