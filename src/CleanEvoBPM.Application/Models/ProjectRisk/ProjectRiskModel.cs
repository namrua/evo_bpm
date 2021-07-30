using System;
using CleanEvoBPM.Domain.Entities;

namespace CleanEvoBPM.Application.Models.ProjectRisk
{
    public class ProjectRiskModel : AuditableEntity
    {
        public Guid ProjectId { get; set; }
    }
}