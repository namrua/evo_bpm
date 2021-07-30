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
    public class GetProjectTypeDetailsHandler : IRequestHandler<GetProjectTypeDetailsQuery, ProjectTypeResponseModel>
    {
        private readonly IProjectTypeDataService _projectTypeDataService;
        public GetProjectTypeDetailsHandler(IProjectTypeDataService projectTypeDataService)
        {
            _projectTypeDataService = projectTypeDataService;
        }

        public async Task<ProjectTypeResponseModel> Handle(GetProjectTypeDetailsQuery request, CancellationToken cancellationToken)
        {
            var result = await _projectTypeDataService.GetProjectTypeDetail(request);
            return result;
        }
    }
}
