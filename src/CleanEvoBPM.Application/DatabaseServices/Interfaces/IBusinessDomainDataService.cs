using System.Collections.Generic;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Command;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Query;
using CleanEvoBPM.Application.Models.BusinessDomain;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{
    public interface IBusinessDomainDataService
    {
         Task<IEnumerable<BusinessDomainResponseModel>> FetchBusinessDomain(FetchBusinessDomainQuery query);
         Task<GenericResponse> CreatBusinessDomain(CreateBusinessDomainCommand request);
         Task<bool> UpdateBusinessDomain(UpdateBusinessDomainCommand requets);
         Task<bool> DeleteBusinessDomain(DeleteBusinessDomainCommand requets);
         Task<BusinessDomainResponseModel> GetBusinessDomainDetail(GetBusinessDomainDetailsQuery request);

         Task<bool> IsUniqueName(string businessDomainName);
    }
}