using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Models.Resource
{
    public class ResourceResponseModel
    {
        public Guid Id { get; set; }
        public Guid ResourceId { get; set; }
        public string Name { get; set; }
        public Guid RoleId { get; set; }
        public string Role { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Percentage { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public bool? IsActived { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? CreatedBy { get; set; }
        public bool? DeleteFlag { get; set; }
    }
}
