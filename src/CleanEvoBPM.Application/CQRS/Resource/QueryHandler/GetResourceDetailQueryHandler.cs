using CleanEvoBPM.Application.CQRS.Resource.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Resource;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Resource.QueryHandler
{
    public class GetResourceDetailQueryHandler: BaseResourceHandler,IRequestHandler<GetResourceDetailQuery, ResourceResponseModel>
    {
        public GetResourceDetailQueryHandler(IResourceService resourceService, IRoleService roleService):base(resourceService, roleService)
        {

        }

        public async Task<ResourceResponseModel> Handle(GetResourceDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await _resourceService.GetResourceDetails(request);
            return result;
        }
    }
}
