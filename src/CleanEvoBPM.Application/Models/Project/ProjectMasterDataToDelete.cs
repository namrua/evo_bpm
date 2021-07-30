using System;

namespace CleanEvoBPM.Application.Models.Project
{
    public class ProjectMasterDataToDelete
    {
        public Guid Id { get; set; }
        public Guid ServiceTypeId { get; set; }
        public Guid ProjectTypeId { get; set; }
        public Guid StatusId { get; set; }
        public Guid ClientId { get; set; }
    }
}