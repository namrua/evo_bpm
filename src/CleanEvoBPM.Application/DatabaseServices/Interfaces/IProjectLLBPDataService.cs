using CleanEvoBPM.Application.Models.ProjectLLBP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{
    public interface IProjectLLBPDataService
    {
        Task<bool> CreateProjectLLBP(IEnumerable<ProjectLLBPModel> request, Guid projectId);
        Task<bool> UpdateProjectLLBP(IEnumerable<ProjectLLBPModel> request, Guid projectId);
        Task<bool> DeleteProjectLLBP(Guid projectId);
        Task<IEnumerable<ProjectLLBPResponseModel>> FetchProjectLLBP(Guid projectId);
    }
}
