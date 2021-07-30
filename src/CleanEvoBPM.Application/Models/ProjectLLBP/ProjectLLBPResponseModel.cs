using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Models.ProjectLLBP
{
    public class ProjectLLBPResponseModel
    {
        public Guid Id { get; set; }
        public string ImpactArea { get; set; }
        public Guid ProblemCategory { get; set; }
        public string ProblemSuccessDescription { get; set; }
        public string LLBPDescription { get; set; }
        public Guid ProjectId { get; set; }
    }
}
