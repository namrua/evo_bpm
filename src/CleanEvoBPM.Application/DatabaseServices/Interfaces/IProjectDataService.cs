using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.Project.Command;
using CleanEvoBPM.Application.CQRS.Project.Query;
using CleanEvoBPM.Application.Models.Project;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{
    public interface IProjectDataService
    {
        Task<bool> CreateProject(CreateProjectCommand request, Guid clientId, Guid projectId);
        Task<bool> UpdateProject(UpdateProjectCommand request);
        Task<bool> DeleteProject(Guid projectId);
        Task<ProjectResponseModel> GetById(Guid id);
        Task<IEnumerable<ProjectResponseModel>> FetchProject(FetchProjectQuery word);
        Task<IEnumerable<ProjectExportModel>> Export(ExportProject exportParams);
        Task<IEnumerable<Guid>> FetchListProjectBusinessDomain(Guid id);
        Task<IEnumerable<Guid>> FetchListProjectMethodology(Guid id);
        Task<IEnumerable<Guid>> FetchListProjectTechnology(Guid id);
        Task<IEnumerable<ProjectResponseModel>> GetProjectsByManagerId(Guid id);
        Task<IEnumerable<ProjectCountByStatusModel>> GetProjectCountByStatus();
    }
}
