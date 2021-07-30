using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Command;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Query;
using CleanEvoBPM.Application.Models.ProjectHolidayPlan;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{
    public interface IProjectHolidayPlanDataService
    {
        Task<GenericResponse> Create(CreateProjectHolidayPlanCommand request);
        Task<GenericResponse> Update(UpdateProjectHolidayPlanCommand request);
        Task<GenericResponse> Delete(Guid id);
        Task<IEnumerable<ProjectHolidayPlanResponseModel>> Get(GetProjectHolidayPlanQuery request);
        Task<ProjectHolidayPlanResponseModel> GetById(Guid id);
    }
}
