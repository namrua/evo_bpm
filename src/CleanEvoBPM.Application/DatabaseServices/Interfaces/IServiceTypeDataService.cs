using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ServiceType.Command;
using CleanEvoBPM.Application.CQRS.ServiceType.Query;
using CleanEvoBPM.Application.Models.ServiceType;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{
    public interface IServiceTypeDataService
    {
        Task<GenericResponse> CreateServiceType(CreateServiceTypeCommand request);
        Task<bool> UpdateServiceType(UpdateServiceTypeCommand request);
        Task<bool> DeleteServiceType(Guid serviceTypeId);
        Task<bool> IsServiceTypeExisted(string ServiceType);
        Task<IEnumerable<ServiceTypeResponseModel>> FetchServiceType(GetServiceTypeQuery request);        
    }
}
