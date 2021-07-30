using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Models.ProjectLLBP
{
    public class ProjectLLBPModel
    {
        public string ImpactArea { get; set; }
        public Guid ProblemCategory { get; set; }
        public string ProblemSuccessDescription { get; set; }
        public string LLBPDescription { get; set; }
    }
}
