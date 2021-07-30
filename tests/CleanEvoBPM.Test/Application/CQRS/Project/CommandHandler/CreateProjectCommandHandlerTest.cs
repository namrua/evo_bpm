using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Query;
using CleanEvoBPM.Application.CQRS.Client.Command;
using CleanEvoBPM.Application.CQRS.Methodology.Query;
using CleanEvoBPM.Application.CQRS.Project.Command;
using CleanEvoBPM.Application.CQRS.Project.CommandHandler;
using CleanEvoBPM.Application.CQRS.ProjectType.Query;
using CleanEvoBPM.Application.CQRS.ServiceType.Query;
using CleanEvoBPM.Application.CQRS.Technology.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.BusinessDomain;
using CleanEvoBPM.Application.Models.Client;
using CleanEvoBPM.Application.Models.Methodology;
using CleanEvoBPM.Application.Models.ProjectLLBP;
using CleanEvoBPM.Application.Models.ProjectMilestone;
using CleanEvoBPM.Application.Models.ProjectType;
using CleanEvoBPM.Application.Models.ServiceType;
using CleanEvoBPM.Application.Models.Technology;
using CleanEvoBPM.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.Project.CommandHandler
{
    public class CreateProjectCommandHandlerTest
    {
        private readonly Mock<IProjectDataService> _mockProjectDataService;
        private readonly Mock<IBusinessDomainDataService> _mockBusinessDomainDataService;
        private readonly Mock<IClientDataService> _mockClientDataService;
        private readonly Mock<IGenericDataService<UpdateProjectCommand>> _mockGenericDataService;
        private readonly Mock<IProjectTypeDataService> _mockProjectTypeDataService;
        private readonly Mock<IServiceTypeDataService> _mockServiceTypeDataService;
        private readonly Mock<ITechnologyDataService> _mockTechnologyDataService;
        private readonly Mock<IMethodologyDataService> _mockMethodologyDataService;
        private readonly Mock<IStatusDataService> _mockStatusDataService;
        private readonly Mock<IProjectRiskDataService> _mockProjectRiskDataService;
        private readonly Mock<IProjectMilestoneDataService> _mockProjectMilestoneDataService;
        private readonly Mock<IProjectLLBPDataService> _mockProjectLLBPDataService;
        private readonly Mock<INotificationDispatcher> _mockNotificationDispatcher;
        private readonly CreateProjectCommandHandler _createProjectCommandHandler;

        public CreateProjectCommandHandlerTest()
        {
            _mockProjectDataService = new Mock<IProjectDataService>();
            _mockBusinessDomainDataService = new Mock<IBusinessDomainDataService>();
            _mockClientDataService = new Mock<IClientDataService>();
            _mockGenericDataService = new Mock<IGenericDataService<UpdateProjectCommand>>();
            _mockProjectTypeDataService = new Mock<IProjectTypeDataService>();
            _mockServiceTypeDataService = new Mock<IServiceTypeDataService>();
            _mockTechnologyDataService = new Mock<ITechnologyDataService>();
            _mockMethodologyDataService = new Mock<IMethodologyDataService>();
            _mockStatusDataService = new Mock<IStatusDataService>();
            _mockProjectRiskDataService = new Mock<IProjectRiskDataService>();
            _mockProjectMilestoneDataService = new Mock<IProjectMilestoneDataService>();
            _mockProjectLLBPDataService = new Mock<IProjectLLBPDataService>();
            _mockNotificationDispatcher = new Mock<INotificationDispatcher>();
            _createProjectCommandHandler = new CreateProjectCommandHandler(
                _mockProjectDataService.Object,
                _mockBusinessDomainDataService.Object,
                _mockClientDataService.Object,
                _mockProjectTypeDataService.Object,
                _mockServiceTypeDataService.Object,
                _mockTechnologyDataService.Object,
                _mockMethodologyDataService.Object,
                _mockStatusDataService.Object,
                _mockProjectRiskDataService.Object,
                _mockProjectMilestoneDataService.Object,
                _mockProjectLLBPDataService.Object,
                _mockNotificationDispatcher.Object
                );
        }

        [Fact]
        public async Task HandleClientExist_CreateProjectCommandSuccess_ReturnGenericResponse()
        {
            //Setup
            var clientId = Guid.NewGuid();
            var clientReturn = new ClientResponseModel
            {
                Id = clientId
            };
            var bussinessReturn = new List<BusinessDomainResponseModel>
            {
                new BusinessDomainResponseModel {
                BusinessDomainName = "Test",
                Status = true
                }
            };
            var command = new CreateProjectCommand() { 
                Client = new CleanEvoBPM.Application.CQRS.Project.Command.Client(),
                MethodologyId = new List<Guid> { Guid.NewGuid() },
                BusinessDomainId = new List<Guid> { Guid.NewGuid()},
                TechnologyId = new List<Guid> { Guid.NewGuid()},
                ServiceTypeId = Guid.NewGuid(),
                ProjectTypeId = Guid.NewGuid()
            };
            //Mock
            _mockClientDataService.Setup(x => x.GetClientByName(It.IsAny<string>()))
                .Returns(Task.FromResult(clientReturn));

            _mockBusinessDomainDataService.Setup(x => x.FetchBusinessDomain(It.IsAny<FetchBusinessDomainQuery>()))
                .Returns(Task.FromResult(bussinessReturn.AsEnumerable()));

            _mockProjectTypeDataService.Setup(x => x.FetchProjectType(It.IsAny<GetProjectTypeQuery>()))
                .Returns(Task.FromResult(new List<ProjectTypeResponseModel>() {
                new ProjectTypeResponseModel { 
                    Id = Guid.NewGuid(),
                    RecordStatus = true
                } }.AsEnumerable()));

            _mockServiceTypeDataService.Setup(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()))
                .Returns(Task.FromResult(new List<ServiceTypeResponseModel>() {
                new ServiceTypeResponseModel {
                    Id = Guid.NewGuid(),
                    RecordStatus = true
                } }.AsEnumerable()));

            _mockMethodologyDataService.Setup(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()))
                .Returns(Task.FromResult(new List<MethodologyResponseModel>() {
                new MethodologyResponseModel {
                    Id = Guid.NewGuid(),
                    RecordStatus = true
                } }.AsEnumerable()));

            _mockTechnologyDataService.Setup(x => x.Fetch(It.IsAny<FetchTechnologyQuery>()))
                .Returns(Task.FromResult(new List<TechnologyResponseModel>(){ new TechnologyResponseModel {
                    Id = Guid.NewGuid(),
                    TechnologyActive = true
                } }.AsEnumerable()));

            _mockProjectRiskDataService.Setup(x => x.CreateManyProjectRisk(It.IsAny<IEnumerable<string>>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(true));

            _mockProjectMilestoneDataService.Setup(x => x.CreateProjectMilestones(It.IsAny<IEnumerable<ProjectMilestoneModel>>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(true));

            _mockProjectLLBPDataService.Setup(x => x.CreateProjectLLBP(It.IsAny<IEnumerable<ProjectLLBPModel>>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(true));

            _mockProjectDataService.Setup(x => x.CreateProject(It.IsAny<CreateProjectCommand>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(true));

            //Run Service
            var result = await _createProjectCommandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.IsType<GenericResponse>(result);
            Assert.True(result.Success);
            Assert.Equal("Create Project Successfully!",result.Message);

            //Verify
            _mockClientDataService.Verify(x => x.GetClientByName(It.IsAny<string>()), Times.Once);
            _mockBusinessDomainDataService.Verify(x => x.FetchBusinessDomain(It.IsAny<FetchBusinessDomainQuery>()), Times.Once);
            _mockProjectDataService.Verify(x => x.CreateProject(It.IsAny<CreateProjectCommand>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
            _mockProjectTypeDataService.Verify(x => x.FetchProjectType(It.IsAny<GetProjectTypeQuery>()), Times.Once);
            _mockServiceTypeDataService.Verify(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()), Times.Once);
            _mockMethodologyDataService.Verify(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()), Times.Once);
            _mockTechnologyDataService.Verify(x => x.Fetch(It.IsAny<FetchTechnologyQuery>()), Times.Once);
        }

        [Fact]
        public async Task HandleClientNotExist_CreateProjectCommandSuccess_ReturnGenericResponse()
        {
            //Setup
            var clientId = Guid.NewGuid();
            var clientReturn = new GenericResponse<ClientResponseModel>
            {
                Success = true,
                Content = new ClientResponseModel()
            };
            var bussinessReturn = new BusinessDomainResponseModel
            {
                BusinessDomainName = "Test",
                Status = true
            };
            var command = new CreateProjectCommand()
            {
                Client = new CleanEvoBPM.Application.CQRS.Project.Command.Client()
            };
            //Mock
            _mockClientDataService.Setup(x => x.GetClientByName(It.IsAny<string>()))
                .Returns(Task.FromResult((ClientResponseModel) null));

            _mockClientDataService.Setup(x => x.CreateClient(It.IsAny<CreateClientCommand>()))
                .Returns(Task.FromResult(clientReturn));

            _mockBusinessDomainDataService.Setup(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()))
                .Returns(Task.FromResult(bussinessReturn));

            _mockProjectTypeDataService.Setup(x => x.FetchProjectType(It.IsAny<GetProjectTypeQuery>()))
                .Returns(Task.FromResult(new List<ProjectTypeResponseModel>() {
                new ProjectTypeResponseModel {
                    Id = Guid.NewGuid(),
                    RecordStatus = true
                } }.AsEnumerable()));

            _mockServiceTypeDataService.Setup(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()))
                .Returns(Task.FromResult(new List<ServiceTypeResponseModel>() {
                new ServiceTypeResponseModel {
                    Id = Guid.NewGuid(),
                    RecordStatus = true
                } }.AsEnumerable()));

            _mockMethodologyDataService.Setup(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()))
                .Returns(Task.FromResult(new List<MethodologyResponseModel>() {
                new MethodologyResponseModel {
                    Id = Guid.NewGuid(),
                    RecordStatus = true
                } }.AsEnumerable()));

            _mockTechnologyDataService.Setup(x => x.GetTechnologyDetail(It.IsAny<GetTechnologyDetailsQuery>()))
                .Returns(Task.FromResult(new TechnologyResponseModel
                {
                    Id = Guid.NewGuid(),
                    TechnologyActive = true
                }));

            _mockProjectDataService.Setup(x => x.CreateProject(command, It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(true));

            //Run Service
            var result = await _createProjectCommandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.IsType<GenericResponse>(result);
            Assert.True(result.Success);
            Assert.Equal("Create Project And Client Successfully!", result.Message);

            //Verify
            _mockClientDataService.Verify(x => x.GetClientByName(It.IsAny<string>()), Times.Once);
            _mockClientDataService.Verify(x => x.CreateClient(It.IsAny<CreateClientCommand>()), Times.Once);
            _mockBusinessDomainDataService.Verify(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()), Times.Once);
            _mockProjectDataService.Verify(x => x.CreateProject(It.IsAny<CreateProjectCommand>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
            _mockProjectTypeDataService.Verify(x => x.FetchProjectType(It.IsAny<GetProjectTypeQuery>()), Times.Once);
            _mockServiceTypeDataService.Verify(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()), Times.Once);
            _mockMethodologyDataService.Verify(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()), Times.Once);
            _mockTechnologyDataService.Verify(x => x.GetTechnologyDetail(It.IsAny<GetTechnologyDetailsQuery>()), Times.Once);
        }

        [Fact]
        public async Task Handle_CreateProjectCommandFailed_ReturnGenericResponse()
        {
            //Setup
            var clientId = Guid.NewGuid();
            var clientReturn = new GenericResponse<ClientResponseModel>
            {
                Success = true,
                Content = new ClientResponseModel()
            };
            var bussinessReturn = new BusinessDomainResponseModel
            {
                BusinessDomainName = "Test",
                Status = true
            };
            var command = new CreateProjectCommand()
            {
                Client = new CleanEvoBPM.Application.CQRS.Project.Command.Client()
            };
            //Mock
            _mockClientDataService.Setup(x => x.GetClientByName(It.IsAny<string>()))
                .Returns(Task.FromResult((ClientResponseModel)null));

            _mockClientDataService.Setup(x => x.CreateClient(It.IsAny<CreateClientCommand>()))
                .Returns(Task.FromResult(clientReturn));

            _mockBusinessDomainDataService.Setup(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()))
                .Returns(Task.FromResult(bussinessReturn));

            _mockBusinessDomainDataService.Setup(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()))
                .Returns(Task.FromResult(bussinessReturn));

            _mockProjectTypeDataService.Setup(x => x.FetchProjectType(It.IsAny<GetProjectTypeQuery>()))
                .Returns(Task.FromResult(new List<ProjectTypeResponseModel>() {
                new ProjectTypeResponseModel {
                    Id = Guid.NewGuid(),
                    RecordStatus = true
                } }.AsEnumerable()));

            _mockServiceTypeDataService.Setup(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()))
                .Returns(Task.FromResult(new List<ServiceTypeResponseModel>() {
                new ServiceTypeResponseModel {
                    Id = Guid.NewGuid(),
                    RecordStatus = true
                } }.AsEnumerable()));

            _mockMethodologyDataService.Setup(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()))
                .Returns(Task.FromResult(new List<MethodologyResponseModel>() {
                new MethodologyResponseModel {
                    Id = Guid.NewGuid(),
                    RecordStatus = true
                } }.AsEnumerable()));

            _mockTechnologyDataService.Setup(x => x.GetTechnologyDetail(It.IsAny<GetTechnologyDetailsQuery>()))
                .Returns(Task.FromResult(new TechnologyResponseModel
                {
                    Id = Guid.NewGuid(),
                    TechnologyActive = true
                }));

            _mockProjectDataService.Setup(x => x.CreateProject(command, It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(false));

            //Run Service
            var result = await _createProjectCommandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.IsType<GenericResponse>(result);
            Assert.False(result.Success);
            Assert.Equal("Create Failed", result.Message);

            //Verify
            _mockClientDataService.Verify(x => x.GetClientByName(It.IsAny<string>()), Times.Once);
            _mockClientDataService.Verify(x => x.CreateClient(It.IsAny<CreateClientCommand>()), Times.Once);
            _mockBusinessDomainDataService.Verify(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()), Times.Once);
            _mockProjectDataService.Verify(x => x.CreateProject(It.IsAny<CreateProjectCommand>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
            _mockProjectTypeDataService.Verify(x => x.FetchProjectType(It.IsAny<GetProjectTypeQuery>()), Times.Once);
            _mockServiceTypeDataService.Verify(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()), Times.Once);
            _mockMethodologyDataService.Verify(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()), Times.Once);
            _mockTechnologyDataService.Verify(x => x.GetTechnologyDetail(It.IsAny<GetTechnologyDetailsQuery>()), Times.Once);
        }

        [Fact]
        public async Task HandleMasterDataInActive_CreateProjectCommandFailed_ReturnGenericResponse()
        {
            //Setup
            var clientId = Guid.NewGuid();
            var clientReturn = new GenericResponse<ClientResponseModel>
            {
                Success = true,
                Content = new ClientResponseModel()
            };
            var bussinessReturn = new BusinessDomainResponseModel
            {
                BusinessDomainName = "Test",
            };
            var command = new CreateProjectCommand()
            {
                Client = new CleanEvoBPM.Application.CQRS.Project.Command.Client()
            };
            //Mock

            _mockBusinessDomainDataService.Setup(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()))
                .Returns(Task.FromResult(bussinessReturn));

            _mockBusinessDomainDataService.Setup(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()))
                .Returns(Task.FromResult(bussinessReturn));

            _mockProjectTypeDataService.Setup(x => x.FetchProjectType(It.IsAny<GetProjectTypeQuery>()))
                .Returns(Task.FromResult(new List<ProjectTypeResponseModel>() {
                new ProjectTypeResponseModel {
                    Id = Guid.NewGuid(),
                    RecordStatus = true
                } }.AsEnumerable()));

            _mockServiceTypeDataService.Setup(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()))
                .Returns(Task.FromResult(new List<ServiceTypeResponseModel>() {
                new ServiceTypeResponseModel {
                    Id = Guid.NewGuid(),
                    RecordStatus = true
                } }.AsEnumerable()));

            _mockMethodologyDataService.Setup(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()))
                .Returns(Task.FromResult(new List<MethodologyResponseModel>() {
                new MethodologyResponseModel {
                    Id = Guid.NewGuid(),
                    RecordStatus = true
                } }.AsEnumerable()));

            _mockTechnologyDataService.Setup(x => x.GetTechnologyDetail(It.IsAny<GetTechnologyDetailsQuery>()))
                .Returns(Task.FromResult(new TechnologyResponseModel
                {
                    Id = Guid.NewGuid(),
                    TechnologyActive = true
                }));

            //Run Service
            var result = await _createProjectCommandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.IsType<GenericResponse>(result);
            Assert.False(result.Success);
            Assert.Equal("Create Project Failed! Some Master Data Is DeActivated! Please Reload The Page!", result.Message);

            //Verify
            _mockBusinessDomainDataService.Verify(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()), Times.Once);
            _mockProjectTypeDataService.Verify(x => x.FetchProjectType(It.IsAny<GetProjectTypeQuery>()), Times.Once);
            _mockServiceTypeDataService.Verify(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()), Times.Once);
            _mockMethodologyDataService.Verify(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()), Times.Once);
            _mockTechnologyDataService.Verify(x => x.GetTechnologyDetail(It.IsAny<GetTechnologyDetailsQuery>()), Times.Once);
        }
    }
}
