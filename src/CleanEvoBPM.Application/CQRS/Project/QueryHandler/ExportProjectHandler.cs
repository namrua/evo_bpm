using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.Project.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Project;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.Project.QueryHandler
{
    public class ExportProjectHandler : BaseProjectHandler, IRequestHandler<ExportProject, IEnumerable<ProjectExportModel>>
    {
        public ExportProjectHandler(IProjectDataService 
            projectDataService, 
            IBusinessDomainDataService businessDomainDataService, 
            IClientDataService clientDataService) : 
            base(projectDataService, 
                businessDomainDataService, 
                clientDataService)
        {
        }

        public async Task<IEnumerable<ProjectExportModel>> Handle(ExportProject request, CancellationToken cancellationToken)
        {
            var result = await _projectDataService.Export(request);
            foreach (var item in result)
            {
                item.LastUpdated = Convert.ToDateTime(item.LastUpdated).ToString("dd-MMM-yyyy");
                item.CreatedDate = Convert.ToDateTime(item.CreatedDate).ToString("dd-MMM-yyyy");
            }
            return result;
        }
    }
}