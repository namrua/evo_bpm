using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Project.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Project;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Project.QueryHandler
{
    public class GetMyProjectByIdQueryHandler : BaseProjectHandler, IRequestHandler<GetMyProjectByIdQuery, GenericResponse<ProjectResponseModel>>
    {
        public GetMyProjectByIdQueryHandler(IProjectDataService projectDataService,
            IBusinessDomainDataService businessDomainDataService,
            IClientDataService clientDataService)
            : base(projectDataService,
                  businessDomainDataService,
                  clientDataService)
        {
        }

        public async Task<GenericResponse<ProjectResponseModel>> Handle(GetMyProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectDataService.GetById(request.ProjectId);
            if (project.ProjectManagerId != request.ManagerId)
            {
                return GenericResponse.FailureResult<ProjectResponseModel>();
            }

            var result = GenericResponse.SuccessResult<ProjectResponseModel>(project);
            result.Code = 200;

            return result;
        }
    }
}
