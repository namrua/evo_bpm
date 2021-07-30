using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Query;
using CleanEvoBPM.Application.CQRS.Client.Query;
using CleanEvoBPM.Application.CQRS.DeliveryLocation.Query;
using CleanEvoBPM.Application.CQRS.DeliveryODC.Query;
using CleanEvoBPM.Application.CQRS.Methodology.Query;
using CleanEvoBPM.Application.CQRS.Project.CommandHandler;
using CleanEvoBPM.Application.CQRS.Project.Query;
using CleanEvoBPM.Application.CQRS.ProjectType.Query;
using CleanEvoBPM.Application.CQRS.ServiceType.Query;
using CleanEvoBPM.Application.CQRS.Status.Query;
using CleanEvoBPM.Application.CQRS.Technology.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.BusinessDomain;
using CleanEvoBPM.Application.Models.Methodology;
using CleanEvoBPM.Application.Models.Project;
using CleanEvoBPM.Application.Models.Technology;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Project.QueryHandler
{
    public class GetProjectByIdHandler : BaseProjectHandler, IRequestHandler<GetProjectById, GenericResponse<ProjectResponseByIdModel>>
    {
        private readonly IProjectTypeDataService _projectTypeDataService;
        private readonly IServiceTypeDataService _serviceTypeDataService;
        private readonly ITechnologyDataService _technologyDataService;
        private readonly IMethodologyDataService _methodologyDataService;
        private readonly IStatusDataService _statusDataService;
        private readonly IDeliveryODCDataService _deliveryODCDataService;
        private readonly IDeliveryLocationDataService _deliveryLocationDataService;
        public GetProjectByIdHandler(IProjectDataService projectDataService,
            IBusinessDomainDataService businessDomainDataService,
            IClientDataService clientDataService,
            IProjectTypeDataService projectTypeDataService,
            IServiceTypeDataService serviceTypeDataService,
            ITechnologyDataService technologyDataService,
            IMethodologyDataService methodologyDataService,
            IStatusDataService statusDataService,
            IDeliveryODCDataService deliveryODCDataService,
            IDeliveryLocationDataService deliveryLocationDataService)
            : base(projectDataService,
                  businessDomainDataService,
                  clientDataService)
        {
            _projectTypeDataService = projectTypeDataService;
            _serviceTypeDataService = serviceTypeDataService;
            _technologyDataService = technologyDataService;
            _methodologyDataService = methodologyDataService;
            _statusDataService = statusDataService;
            _deliveryODCDataService = deliveryODCDataService;
            _deliveryLocationDataService = deliveryLocationDataService;
        }
        public async Task<GenericResponse<ProjectResponseByIdModel>> Handle(GetProjectById request, CancellationToken cancellationToken)
        {
            try
            {
                var result = new GenericResponse<ProjectResponseByIdModel>()
                {
                    Success = false,
                    Message = "Cannot Get Data Of Project"
                };
                var project = await _projectDataService.GetById(request.Id);
                if (project != null)
                {
                    var client = await _clientDataService.GetClient(new GetClientDetailQuery { Id = project.ClientId });
                    var projectType = await _projectTypeDataService.FetchProjectType(
                        new GetProjectTypeQuery { Id = project.ProjectTypeId });
                    var serviceType = await _serviceTypeDataService.FetchServiceType(
                        new GetServiceTypeQuery { Id = project.ServiceTypeId });
                    var status = await _statusDataService.GetStatusDetails(
                        new GetStatusDetailsQuery { Id = project.StatusId });
                    var deliveryODC = await _deliveryODCDataService.GetById(project.DeliveryODCId);
                    var deliveryLocation = await _deliveryLocationDataService.GetById(project.DeliveryLocationId);

                    var businessDomains = await _businessDomainDataService.FetchBusinessDomain(
                        new FetchBusinessDomainQuery());
                    var technologies = await _technologyDataService.Fetch(
                        new FetchTechnologyQuery());
                    var methodologies = await _methodologyDataService.FetchMethodology(
                        new GetMethodologyQuery());

                    var listBusinessDomainId = await _projectDataService.FetchListProjectBusinessDomain(request.Id);
                    var listTechnologyId = await _projectDataService.FetchListProjectTechnology(request.Id);
                    var listMethodologyId = await _projectDataService.FetchListProjectMethodology(request.Id);

                    var businessDomainToReturn = GetListModel<BusinessDomainResponseModel>
                        .Get(businessDomains, listBusinessDomainId.ToList());
                    var technologyToReturn = GetListModel<TechnologyResponseModel>
                        .Get(technologies, listTechnologyId.ToList());
                    var methodologyToReturn = GetListModel<MethodologyResponseModel>
                        .Get(methodologies, listMethodologyId.ToList());

                    if (client != null
                        && businessDomainToReturn != null
                        && projectType != null
                        && serviceType != null
                        && technologyToReturn != null
                        && methodologyToReturn != null)
                    {
                        result.Content = new ProjectResponseByIdModel();
                        result.Success = true;
                        result.Message = "Get Project Successfully";
                        result.Content.Id = project.Id;
                        result.Content.ProjectName = project.ProjectName;
                        result.Content.ProjectCode = project.ProjectCode;
                        result.Content.ProjectManagerId = project.ProjectManagerId;
                        result.Content.StartDate = project.StartDate;
                        result.Content.LastUpdated = project.LastUpdated;
                        result.Content.Client = client;
                        result.Content.BusinessDomains = businessDomainToReturn;
                        result.Content.ProjectType = projectType.FirstOrDefault();
                        result.Content.ServiceType = serviceType.FirstOrDefault();
                        result.Content.Technologies = technologyToReturn;
                        result.Content.Methodologies = methodologyToReturn;
                        result.Content.Status = status.Content;
                        result.Content.DeliveryODC = deliveryODC;
                        result.Content.DeliveryLocation = deliveryLocation;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
