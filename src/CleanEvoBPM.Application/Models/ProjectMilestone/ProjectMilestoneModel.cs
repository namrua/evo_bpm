using CleanEvoBPM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Models.ProjectMilestone
{
    public class ProjectMilestoneModel 
    {
        public Guid Id { get; set; }
        public Guid UpdatedUser { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public Guid ProjectId { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
    }
}
