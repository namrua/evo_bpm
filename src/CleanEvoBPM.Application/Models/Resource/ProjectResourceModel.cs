using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Models.Resource
{
    public class ProjectResourceModel
    {
        public Guid ProjectId { get; set; }
        public Guid ResourceId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Percentage { get; set; }
        public Guid RoleId { get; set; }
    }
}
