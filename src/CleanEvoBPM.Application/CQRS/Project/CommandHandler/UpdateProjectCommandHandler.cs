using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Query;
using CleanEvoBPM.Application.CQRS.Client.Command;
using CleanEvoBPM.Application.CQRS.Methodology.Query;
using CleanEvoBPM.Application.CQRS.Project.Command;
using CleanEvoBPM.Application.CQRS.Project.Event;
using CleanEvoBPM.Application.CQRS.ProjectType.Query;
using CleanEvoBPM.Application.CQRS.ServiceType.Query;
using CleanEvoBPM.Application.CQRS.Technology.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.BusinessDomain;
using CleanEvoBPM.Application.Models.Methodology;
using CleanEvoBPM.Application.Models.Project;
using CleanEvoBPM.Application.Models.ProjectType;
using CleanEvoBPM.Application.Models.ServiceType;
using CleanEvoBPM.Application.Models.Technology;
using CleanEvoBPM.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Project.CommandHandler
{
    public class UpdateProjectCommandHandler : BaseProjectHandler, IRequestHandler<UpdateProjectCommand, GenericResponse>
    {
        private readonly IGenericDataService<UpdateProjectCommand> _genericDataService;
        private readonly IProjectTypeDataService _projectTypeDataService;
        private readonly IServiceTypeDataService _serviceTypeDataService;
        private readonly ITechnologyDataService _technologyDataService;
        private readonly IMethodologyDataService _methodologyDataService;
        private readonly IStatusDataService _statusDataService;
        private readonly IProjectRiskDataService _projectRiskDataService;

        private readonly IProjectMilestoneDataService _projectMilestoneDataService;
        private readonly IProjectLLBPDataService _projectLLBPDataService;
        private readonly INotificationDispatcher _dispatcher;
        public UpdateProjectCommandHandler(IProjectDataService projectDataService,
            IBusinessDomainDataService businessDomainDataService,
            IClientDataService clientDataService,
            IGenericDataService<UpdateProjectCommand> genericDataService,
            IProjectTypeDataService projectTypeDataService,
            IServiceTypeDataService serviceTypeDataService,
            ITechnologyDataService technologyDataService,
            IMethodologyDataService methodologyDataService,
            IStatusDataService statusDataService,
            IProjectRiskDataService projectRiskDataService,
            IProjectMilestoneDataService projectMilestoneDataService,
            IProjectLLBPDataService projectLLBPDataService,
            INotificationDispatcher dispatcher)
            : base(projectDataService,
                  businessDomainDataService,
                  clientDataService)
        {
            _genericDataService = genericDataService;
            _projectTypeDataService = projectTypeDataService;
            _serviceTypeDataService = serviceTypeDataService;
            _technologyDataService = technologyDataService;
            _methodologyDataService = methodologyDataService;
            _statusDataService = statusDataService;
            _projectRiskDataService = projectRiskDataService;
            _projectMilestoneDataService = projectMilestoneDataService;
            _projectLLBPDataService = projectLLBPDataService;
            _dispatcher = dispatcher;
        }

        public async Task<GenericResponse> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            GenericResponse result = new GenericResponse()
            {
                Success = false,
                Message = "Updated Failed!"
            };
            GenericResponse errorInactiveStatus = new GenericResponse()
            {
                Success = false,
                Code = 400,
                Message = "One Or More {0} is Deactivated! Updated Failed!"
            };

            var listProject = _genericDataService.GetAll("Project").Result;
            var listProjectToCheck = listProject.ToList();
            var itemBeEdited = listProject.Single(r => r.Id == request.Id);
            listProjectToCheck.Remove(itemBeEdited);

            for(int i = 0; i< listProjectToCheck.Count; i++)
            {
                if(request.ProjectName == listProjectToCheck[i].ProjectName 
                    || request.ProjectCode == listProjectToCheck[i].ProjectCode)
                {
                    result.Code = 400;
                    result.Message = "ProjectName or ProjectCode is dupplicated!";
                    return result;
                }
            }
            var projectToCheckInActiveMD = await base._projectDataService.GetById(request.Id);

            var projectType = await _projectTypeDataService.FetchProjectType(
                    new GetProjectTypeQuery { Id = request.ProjectTypeId });
            var serviceType = await _serviceTypeDataService.FetchServiceType(
                new GetServiceTypeQuery { Id = request.ServiceTypeId });

            var businessDomains = await _businessDomainDataService.FetchBusinessDomain(new FetchBusinessDomainQuery());
            var methodologies = await _methodologyDataService.FetchMethodology(new GetMethodologyQuery());
            var technologies = await _technologyDataService.Fetch(new FetchTechnologyQuery());
            var businessToCheck = GetListModel<BusinessDomainResponseModel>
                .Get(businessDomains, request.BusinessDomainId);
            var methodologiesToCheck = GetListModel<MethodologyResponseModel>
                .Get(methodologies, request.MethodologyId);
            var technologiesToCheck = GetListModel<TechnologyResponseModel>
                .Get(technologies, request.TechnologyId);

            var businessDomainPast = await _projectDataService.FetchListProjectBusinessDomain(request.Id);
            var methodologiesPast = await _projectDataService.FetchListProjectMethodology(request.Id);
            var technologiesPast = await _projectDataService.FetchListProjectTechnology(request.Id);

            if (!CheckStatusMasterData<BusinessDomainResponseModel>.Checking(businessDomainPast, businessToCheck, "Status"))
            {
                errorInactiveStatus.Message = string.Format(errorInactiveStatus.Message, "BusinessDomain");
                return errorInactiveStatus;
            }

            if (!CheckStatusMasterData<MethodologyResponseModel>.Checking(methodologiesPast, methodologiesToCheck, "RecordStatus"))
            {
                errorInactiveStatus.Message = string.Format(errorInactiveStatus.Message, "Methodology");
                return errorInactiveStatus;
            }

            if (!CheckStatusMasterData<TechnologyResponseModel>.Checking(technologiesPast, technologiesToCheck, "TechnologyActive"))
            {
                errorInactiveStatus.Message = string.Format(errorInactiveStatus.Message, "Technology");
                return errorInactiveStatus;
            }

            if (request.ProjectTypeId != projectToCheckInActiveMD.ProjectTypeId)
            {
                if (!CheckStatus<ProjectTypeResponseModel>.Checking(projectType.FirstOrDefault(), "RecordStatus"))
                {
                    errorInactiveStatus.Message = string.Format(errorInactiveStatus.Message, "ProjectType");
                    return errorInactiveStatus;
                }
            }
            
            if (request.ServiceTypeId != projectToCheckInActiveMD.ServiceTypeId)
            {
                if (!CheckStatus<ServiceTypeResponseModel>.Checking(serviceType.FirstOrDefault(), "RecordStatus"))
                {
                    errorInactiveStatus.Message = string.Format(errorInactiveStatus.Message, "ServiceType");
                    return errorInactiveStatus;
                }
            }
            var projectUpdateResult = await _projectDataService.UpdateProject(request);
            await _dispatcher.Push(new UpdateProjectLog(request));
            if (request.Milestones != null
                && request.Milestones.Any())
            {  
                await _projectMilestoneDataService.DeleteProjectMilestones(request.Id);
                await _projectMilestoneDataService.CreateProjectMilestones(request.Milestones, request.Id);
            }
            else
            {
                await _projectMilestoneDataService.DeleteProjectMilestones(request.Id);
            }

            if(request.Risks != null
                && request.Risks.Any())
            {
                await _projectRiskDataService.DeleteProjectRisk(request.Id);
                await _projectRiskDataService.CreateManyProjectRisk(request.Risks, request.Id);
            }
            else
            {
                await _projectRiskDataService.DeleteProjectRisk(request.Id);
            }

            if(request.LessonLearntBestPractives != null
                && request.LessonLearntBestPractives.Any())
            {
                await _projectLLBPDataService.UpdateProjectLLBP(request.LessonLearntBestPractives, request.Id);
            }
            else
            {
                await _projectLLBPDataService.DeleteProjectLLBP(request.Id);
            }

            if (projectUpdateResult)
            {
                result.Success = true;
                result.Code = 200;
                result.Message = "Updated Successfully!";
            }
            return result;
        }
    }

    public class CheckStatusMasterData<T>
    {
        public static bool Checking(IEnumerable<Guid> listPresent, List<T> listNew, string statusName)
        {
            foreach(var item in listPresent)
            {
                return CheckOne(item, listNew, statusName);
            }
            return true;
        }

        private static bool CheckOne(Guid id, List<T> listNew, string statusName)
        {
            foreach(var item in listNew)
            {
                Guid.TryParse(item.GetType().GetProperty("Id").GetValue(item, null).ToString(), out var idN);
                if (id != idN)
                {
                    return CheckStatus<T>.Checking(item, statusName);
                }
            }
            return true;
        }
    }
}
