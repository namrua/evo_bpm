using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Resource.Command;
using CleanEvoBPM.Application.CQRS.Resource.Query;
using CleanEvoBPM.Application.Models.Resource;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{
    public interface IResourceService
    {
        Task<IEnumerable<ResourceResponseModel>> Fetch(FetchResourceQuery query);
        Task<ResourceResponseModel> GetResourceDetails(GetResourceDetailQuery query);
        Task<bool> CreateResource(CreateResourceCommand command);
        Task<bool> Update(UpdateResourceCommand command);
        Task<bool> Delete(DeleteResourceCommand command);
    }
}
