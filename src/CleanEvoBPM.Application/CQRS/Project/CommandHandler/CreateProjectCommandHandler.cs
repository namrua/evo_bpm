using System;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.Project.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using MediatR;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Query;
using CleanEvoBPM.Application.CQRS.Client.Command;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProjectType.Query;
using CleanEvoBPM.Application.CQRS.ServiceType.Query;
using CleanEvoBPM.Application.CQRS.Methodology.Query;
using CleanEvoBPM.Application.CQRS.Technology.Query;
using CleanEvoBPM.Application.Models.ProjectType;
using System.Linq;
using CleanEvoBPM.Application.Models.ServiceType;
using CleanEvoBPM.Application.Models.Methodology;
using CleanEvoBPM.Application.Models.Technology;
using CleanEvoBPM.Application.Models.BusinessDomain;
using System.Collections.Generic;
using CleanEvoBPM.Domain;
using CleanEvoBPM.Application.CQRS.Project.Event;
using CleanEvoBPM.Application.CQRS.Client.Event;

namespace CleanEvoBPM.Application.CQRS.Project.CommandHandler
{
    public class CreateProjectCommandHandler : BaseProjectHandler, IRequestHandler<CreateProjectCommand, GenericResponse>
    {
        private readonly IProjectTypeDataService _projectTypeDataService;
        private readonly IServiceTypeDataService _serviceTypeDataService;
        private readonly ITechnologyDataService _technologyDataService;
        private readonly IMethodologyDataService _methodologyDataService;
        private readonly IStatusDataService _statusDataService;
        private readonly IProjectRiskDataService _projectRiskDataService;
        private readonly IProjectMilestoneDataService _projectMilestoneDataService;
        private readonly IProjectLLBPDataService _projectLLBPDataService;
        private readonly INotificationDispatcher _dispatcher;
        public CreateProjectCommandHandler(IProjectDataService projectDataService,
            IBusinessDomainDataService businessDomainDataService,
            IClientDataService clientDataService,
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
            _projectTypeDataService = projectTypeDataService;
            _serviceTypeDataService = serviceTypeDataService;
            _technologyDataService = technologyDataService;
            _methodologyDataService = methodologyDataService;
            _statusDataService = statusDataService;
            _projectRiskDataService = projectRiskDataService;
            _projectLLBPDataService = projectLLBPDataService;
            _projectMilestoneDataService = projectMilestoneDataService;
            _dispatcher = dispatcher;
        }

        public async Task<GenericResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            GenericResponse result = new GenericResponse()
            {
                Success = false,
                Message = "Create Failed"
            };

            var projectId = Guid.NewGuid();
            var isClientExist = await base._clientDataService.GetClientByName(request.Client.ClientName);
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

            if (!CheckStatus<ProjectTypeResponseModel>.Checking(projectType.FirstOrDefault(), "RecordStatus"))
            {
                result.Success = false;
                result.Code = 400;
                result.Message = "This ProjectType Is Deactivated! Create Project Failed!";
                return result;
            }

            if (!CheckStatus<ServiceTypeResponseModel>.Checking(serviceType.FirstOrDefault(), "RecordStatus"))
            {
                result.Success = false;
                result.Code = 400;
                result.Message = "This ServiceType Is Deactivated! Create Project Failed!";
                return result;
            }

            if (!CheckListStatus<BusinessDomainResponseModel>.Checking(businessToCheck, "Status"))
            {
                result.Success = false;
                result.Code = 400;
                result.Message = "One Or More BusinessDomain Is Deactivated! Create Project Failed!";
                return result;
            }

            if (!CheckListStatus<MethodologyResponseModel>.Checking(methodologiesToCheck, "RecordStatus"))
            {
                result.Success = false;
                result.Code = 400;
                result.Message = "One Or More Methodology Is Deactivated! Create Project Failed!";
                return result;
            }

            if (!CheckListStatus<TechnologyResponseModel>.Checking(technologiesToCheck, "TechnologyActive"))
            {
                result.Success = false;
                result.Code = 400;
                result.Message = "One Or More Methodology Is Deactivated! Create Project Failed!";
                return result;
            }
            var data = new CreateProjectCommand2
            {
                Id = projectId,
                ClientId = isClientExist.Id,
                ProjectName = request.ProjectName,
                ProjectCode = request.ProjectCode,
                ProjectManagerId = request.ProjectManagerId,
                ProjectTypeId = request.ProjectTypeId,
                ServiceTypeId = request.ServiceTypeId,
                StatusId = request.StatusId,
                DeliveryODCId = request.DeliveryODCId,
                DeliveryLocationId = request.DeliveryLocationId,
                StartDate = request.StartDate,
                LastUpdated = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow
            };

            if (isClientExist != null)
            {
                var projectInsertResult = await _projectDataService.CreateProject(request, isClientExist.Id, projectId);
                await _dispatcher.Push(new CreateProjectLog(data, isClientExist.Id));
                var projectRiskRecordsResult = await _projectRiskDataService.CreateManyProjectRisk(request.Risks, projectId);
                var projectLLBPRecordInsertResult = await _projectLLBPDataService.CreateProjectLLBP(request.LessonLearntBestPractives, projectId);
                var projectMilestonesRecordsResult = await _projectMilestoneDataService.CreateProjectMilestones(request.Milestones, projectId);
                if (projectInsertResult
                    && projectRiskRecordsResult
                    && projectMilestonesRecordsResult
                    && projectLLBPRecordInsertResult)
                {
                    result.Success = true;
                    result.Code = 200;
                    result.Message = "Create Project Successfully!";
                }
            }
            else
            {
                var clientInsert = new CreateClientCommand()
                {
                    ClientName = request.Client.ClientName,
                    ClientDivisionName = request.Client.ClientDivisionName
                };
                var clientInsertResult = await base._clientDataService.CreateClient(clientInsert);
                if (clientInsertResult.Success)
                {
                    await _dispatcher.Push(new CreateClientLog(clientInsert));
                    var projectInsertResult = await _projectDataService.CreateProject(request, clientInsertResult.Content.Id, projectId);
                    await _dispatcher.Push(new CreateProjectLog(data, isClientExist.Id));
                    if (request.Risks != null
                        && request.Risks.Any())
                    {
                        await _projectRiskDataService.CreateManyProjectRisk(request.Risks, projectId);
                    }
                    if(request.Milestones != null
                        && request.Milestones.Any())
                    {
                        await _projectMilestoneDataService.CreateProjectMilestones(request.Milestones, projectId);
                    }
                    if(request.LessonLearntBestPractives != null
                        && request.LessonLearntBestPractives.Any())
                    {
                        await _projectLLBPDataService.CreateProjectLLBP(request.LessonLearntBestPractives, projectId);
                    }
                    if (projectInsertResult)
                    {
                        result.Success = true;
                        result.Code = 200;
                        result.Message = "Create Project And Client Successfully!";
                    }
                }
            }
            return result;
        }
    }
    public class CheckStatus<T>
    {
        public static bool Checking(T entity, string statusName)
        {
            var status = entity.GetType().GetProperty(statusName).GetValue(entity, null).ToString();
            Boolean.TryParse(status, out var result);
            return result;
        }
    }

    public class CheckListStatus<T>
    {
        public static bool Checking(List<T> entities, string statusName)
        {
            var result = true;
            foreach(var item in entities)
            {
                var status = item.GetType().GetProperty(statusName).GetValue(item, null).ToString();
                Boolean.TryParse(status, out var parseResult);
                if (!parseResult) return parseResult;
            }
            return result;
        }
    }

    public class GetListModel<T>
    {
        public static List<T> Get(IEnumerable<T> ls, List<Guid> ids)
        {
            List<T> result = new List<T>();
            foreach(var item in ls)
            {
                Compare(item, ids, result);
            }
            return result;
        }

        public static void Compare(T item, List<Guid> ids, List<T> result)
        {
            foreach (var id in ids)
            {
                Guid.TryParse(item
                    .GetType()
                    .GetProperty("Id")
                    .GetValue(item, null)
                    .ToString()
                    , out var i);
                if (i == id)
                {
                    result.Add(item);
                }
            }
        }
    }
}
