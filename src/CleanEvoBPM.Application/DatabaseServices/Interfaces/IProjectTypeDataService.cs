using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProjectType.Command;
using CleanEvoBPM.Application.CQRS.ProjectType.Query;
using CleanEvoBPM.Application.Models.ProjectType;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{
    public interface IProjectTypeDataService
    {
        Task<GenericResponse> CreateProjectType(CreateProjectTypeCommand request);
        Task<GenericResponse> UpdateProjectType(UpdateProjectTypeCommand request);
        Task<bool> DeleteProjectType(Guid projectId);
        Task<bool> IsProjectTypeExisted(string projectTypeName);        
        Task <ProjectTypeResponseModel> GetProjectTypeDetail(GetProjectTypeDetailsQuery request);
        Task<IEnumerable<ProjectTypeResponseModel>> FetchProjectType(GetProjectTypeQuery request);
    }
}
