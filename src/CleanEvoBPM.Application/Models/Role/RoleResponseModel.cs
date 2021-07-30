using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Models.Role
{
    public class RoleResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsActived { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? CreatedBy { get; set; }
        public bool? DeleteFlag { get; set; }
    }
}
