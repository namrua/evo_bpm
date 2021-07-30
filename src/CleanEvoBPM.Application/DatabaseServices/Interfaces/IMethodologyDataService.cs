using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Methodology.Command;
using CleanEvoBPM.Application.CQRS.Methodology.Query;
using CleanEvoBPM.Application.Models.Methodology;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{   
    public interface IMethodologyDataService
    {
        Task<GenericResponse> CreateMethodology(CreateMethodologyCommand request);
        Task<bool> UpdateMethodology(UpdateMethodologyCommand request);
        Task<bool> DeleteMethodology(Guid methodologyId);        
        Task<bool> IsMethodologyExisted(string methodologyName);
        Task<IEnumerable<MethodologyResponseModel>> FetchMethodology(GetMethodologyQuery request);
    }
}
