using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Technology.Command;
using CleanEvoBPM.Application.CQRS.Technology.Query;
using CleanEvoBPM.Application.Models.Technology;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{
    public interface ITechnologyDataService
    {
        Task<GenericResponse> Create(CreateTechnologyCommand request);
        Task<bool> Delete(Guid technologyId);
        Task<IEnumerable<TechnologyResponseModel>> Fetch(FetchTechnologyQuery query);
        Task<TechnologyResponseModel> GetTechnologyDetail(GetTechnologyDetailsQuery query);
        Task<GenericResponse> Update(UpdateTechnologyCommand request);
    }
}
