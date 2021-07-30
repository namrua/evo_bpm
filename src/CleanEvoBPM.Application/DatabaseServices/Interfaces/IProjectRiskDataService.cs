using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Models.ProjectRisk;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{
    public interface IProjectRiskDataService
    {
         Task<bool> CreateManyProjectRisk(IEnumerable<string> risks, Guid projectId);
         Task<IEnumerable<ProjectRiskModel>> GetProjectRisks(Guid projectId);
         Task<bool> DeleteProjectRisk(Guid projectId);
    }
}