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
    public class FetchResourceQueryHandler:BaseResourceHandler, IRequestHandler<FetchResourceQuery, IEnumerable<ResourceResponseModel>>
    {
        public FetchResourceQueryHandler(IResourceService resourceService, IRoleService roleService) : base(resourceService, roleService)
        {

        }

        public async Task<IEnumerable<ResourceResponseModel>> Handle(FetchResourceQuery request, CancellationToken cancellationToken)
        {
            var result = await _resourceService.Fetch(request);
            return result;
        }
    }
}
