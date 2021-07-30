using CleanEvoBPM.Application.Models.ProjectMilestone;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{
    public interface IProjectMilestoneDataService
    {
        Task<bool> CreateProjectMilestones(IEnumerable<ProjectMilestoneModel> milestones, Guid projectId);
        Task<bool> DeleteProjectMilestones(Guid projectId);
        Task<IEnumerable<ProjectMilestoneModel>> GetProjectMilestones(Guid projectId);
    }
}
