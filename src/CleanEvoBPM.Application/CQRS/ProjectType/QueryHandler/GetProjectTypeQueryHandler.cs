using CleanEvoBPM.Application.CQRS.ProjectType.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.ProjectType.QueryHandler
{
    public class GetProjectTypeQueryHandler : BaseProjectTypeHandler, IRequestHandler<GetProjectTypeQuery, IEnumerable<ProjectTypeResponseModel>>
    {
        public GetProjectTypeQueryHandler(IProjectTypeDataService projectTypeDataService) : base(projectTypeDataService)
        {
        }

        public async Task<IEnumerable<ProjectTypeResponseModel>> Handle(GetProjectTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await _projectTypeDataService.FetchProjectType(request);
            return result;
        }
    }
}
