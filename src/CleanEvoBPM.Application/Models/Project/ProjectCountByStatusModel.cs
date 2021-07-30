using System;

namespace CleanEvoBPM.Application.Models.Project
{
    public class ProjectCountByStatusModel
    {
        public Guid StatusId { get; set; }
        public string Status { get; set; }
        public int ProjectCount { get; set; }
    }
}
