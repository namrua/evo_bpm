using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Query;
using CleanEvoBPM.Application.CQRS.Methodology.Query;
using CleanEvoBPM.Application.CQRS.Project.Command;
using CleanEvoBPM.Application.CQRS.Project.CommandHandler;
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
    public class UpdateProjectCommandHandlerTest
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
        private readonly UpdateProjectCommandHandler _updateProjectCommandHandler;
        private readonly Mock<IProjectRiskDataService> _mockProjectRiskDataService;
        private readonly Mock<IProjectMilestoneDataService> _mockProjectMilestoneDataService;
        private readonly Mock<IProjectLLBPDataService> _mockProjectLLBPDataService;
        private readonly Mock<INotificationDispatcher> _mockNotificationDispatcher;
        public UpdateProjectCommandHandlerTest()
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
            _mockStatusDataService = new Mock<IStatusDataService>();
            _mockProjectRiskDataService = new Mock<IProjectRiskDataService>();
            _mockProjectMilestoneDataService = new Mock<IProjectMilestoneDataService>();
            _mockProjectLLBPDataService = new Mock<IProjectLLBPDataService>();
            _mockNotificationDispatcher = new Mock<INotificationDispatcher>();
            _updateProjectCommandHandler = new UpdateProjectCommandHandler(
                _mockProjectDataService.Object,
                _mockBusinessDomainDataService.Object,
                _mockClientDataService.Object,
                _mockGenericDataService.Object,
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
        public async Task Handle_UpdateProjectCommandSuccess_ReturnGenericResponse()
        {
            //Setup
            var bussinessReturn = new BusinessDomainResponseModel
            {
                BusinessDomainName = "Test",
                Status = true
            };
            var command = new UpdateProjectCommand { Id = Guid.NewGuid(),
                ProjectName = "Test",
                ProjectCode = "Test"
            };
            var listProjectReturn = new List<UpdateProjectCommand>
            {
                command,
                new UpdateProjectCommand { Id = Guid.NewGuid() },
                new UpdateProjectCommand { Id = Guid.NewGuid() }
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            _mockBusinessDomainDataService.Setup(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()))
                .Returns(Task.FromResult(bussinessReturn));

            _mockProjectDataService.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ProjectResponseModel()));

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

            _mockProjectDataService.Setup(x => x.UpdateProject(command))
                .Returns(Task.FromResult(true));

            //Run Service
            var result = await _updateProjectCommandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.IsType<GenericResponse>(result);
            Assert.True(result.Success);
            Assert.Equal("Updated Successfully!", result.Message);

            //Verify
            _mockGenericDataService.Verify(x => x.GetAll(It.IsAny<string>()), Times.Once);
            _mockBusinessDomainDataService.Verify(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()), Times.Once);
            _mockProjectDataService.Verify(x => x.UpdateProject(command), Times.Once);
        }

        [Fact]
        public async Task Handle_UpdateProjectCommandFailed_ReturnGenericResponse()
        {
            //Setup
            var bussinessReturn = new BusinessDomainResponseModel
            {
                BusinessDomainName = "Test"
            };
            var command = new UpdateProjectCommand
            {
                Id = Guid.NewGuid(),
                ProjectName = "Test",
                ProjectCode = "Test"
            };
            var listProjectReturn = new List<UpdateProjectCommand>
            {
                command,
                new UpdateProjectCommand { Id = Guid.NewGuid() },
                new UpdateProjectCommand { Id = Guid.NewGuid() }
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            _mockBusinessDomainDataService.Setup(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()))
                .Returns(Task.FromResult(bussinessReturn));

            _mockProjectDataService.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ProjectResponseModel()));

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

            _mockProjectDataService.Setup(x => x.UpdateProject(command))
                .Returns(Task.FromResult(false));

            //Run Service
            var result = await _updateProjectCommandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.IsType<GenericResponse>(result);
            Assert.False(result.Success);
            Assert.Equal("Updated Failed!", result.Message);

            //Verify
            _mockGenericDataService.Verify(x => x.GetAll(It.IsAny<string>()), Times.Once);
            _mockBusinessDomainDataService.Verify(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()), Times.Once);
            _mockProjectDataService.Verify(x => x.UpdateProject(command), Times.Once);
        }

        [Fact]
        public async Task Handle_UpdateProjectCommandDupplicated_ReturnGenericResponse()
        {
            //Setup
            var bussinessReturn = new BusinessDomainResponseModel
            {
                BusinessDomainName = "Test"
            };
            var command = new UpdateProjectCommand
            {
                Id = Guid.NewGuid()
            };
            var listProjectReturn = new List<UpdateProjectCommand>
            {
                command,
                new UpdateProjectCommand { Id = Guid.NewGuid() },
                new UpdateProjectCommand { Id = Guid.NewGuid() }
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));
            

            //Run Service
            var result = await _updateProjectCommandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.IsType<GenericResponse>(result);
            Assert.False(result.Success);
            Assert.Equal("ProjectName or ProjectCode is dupplicated!", result.Message);

            //Verify
            _mockGenericDataService.Verify(x => x.GetAll(It.IsAny<string>()), Times.Once);
            
        }

        [Fact]
        public async Task Handle_UpdateProjectCommandInactiveBizDMMasterData_ReturnGenericResponse()
        {
            //Setup
            var bussinessReturn = new BusinessDomainResponseModel
            {
                Id = Guid.NewGuid(),
                BusinessDomainName = "Test",
                Status = false
            };
            var command = new UpdateProjectCommand
            {
                Id = Guid.NewGuid(),
                ProjectName = "Test",
                ProjectCode = "Test"
            };
            var listProjectReturn = new List<UpdateProjectCommand>
            {
                command,
                new UpdateProjectCommand { Id = Guid.NewGuid() },
                new UpdateProjectCommand { Id = Guid.NewGuid() }
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            _mockBusinessDomainDataService.Setup(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()))
                .Returns(Task.FromResult(bussinessReturn));

            _mockProjectDataService.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ProjectResponseModel() {BusinessDomainId = Guid.NewGuid() }));

            //Run Service
            var result = await _updateProjectCommandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.IsType<GenericResponse>(result);
            Assert.False(result.Success);
            Assert.Equal("Updated Failed! Some Master Data Is DeActivated! Please Reload The Page!", result.Message);

            //Verify
            _mockGenericDataService.Verify(x => x.GetAll(It.IsAny<string>()), Times.Once);
            _mockBusinessDomainDataService.Verify(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()), Times.Once);
            _mockProjectDataService.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UpdateProjectCommandInactivePTMasterData_ReturnGenericResponse()
        {
            //Setup
            var bussinessReturn = new BusinessDomainResponseModel
            {
                Id = Guid.NewGuid(),
                BusinessDomainName = "Test",
                Status = true
            };
            var command = new UpdateProjectCommand
            {
                Id = Guid.NewGuid(),
                ProjectName = "Test",
                ProjectCode = "Test",
                ProjectTypeId = Guid.NewGuid()
            };
            var listProjectReturn = new List<UpdateProjectCommand>
            {
                command,
                new UpdateProjectCommand { Id = Guid.NewGuid() },
                new UpdateProjectCommand { Id = Guid.NewGuid() }
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            _mockBusinessDomainDataService.Setup(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()))
                .Returns(Task.FromResult(bussinessReturn));

            _mockProjectDataService.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ProjectResponseModel()));

            _mockProjectTypeDataService.Setup(x => x.FetchProjectType(It.IsAny<GetProjectTypeQuery>()))
                .Returns(Task.FromResult(new List<ProjectTypeResponseModel>() {
                new ProjectTypeResponseModel {
                    Id = Guid.NewGuid(),
                    RecordStatus = false
                } }.AsEnumerable()));

            //Run Service
            var result = await _updateProjectCommandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.IsType<GenericResponse>(result);
            Assert.False(result.Success);
            Assert.Equal("Updated Failed! Some Master Data Is DeActivated! Please Reload The Page!", result.Message);

            //Verify
            _mockGenericDataService.Verify(x => x.GetAll(It.IsAny<string>()), Times.Once);
            _mockBusinessDomainDataService.Verify(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()), Times.Once);
            _mockProjectDataService.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mockProjectTypeDataService.Verify(x => x.FetchProjectType(It.IsAny<GetProjectTypeQuery>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UpdateProjectCommandInactiveSTMasterData_ReturnGenericResponse()
        {
            //Setup
            var bussinessReturn = new BusinessDomainResponseModel
            {
                Id = Guid.NewGuid(),
                BusinessDomainName = "Test",
                Status = true
            };
            var command = new UpdateProjectCommand
            {
                Id = Guid.NewGuid(),
                ProjectName = "Test",
                ProjectCode = "Test",
                ServiceTypeId = Guid.NewGuid()
            };
            var listProjectReturn = new List<UpdateProjectCommand>
            {
                command,
                new UpdateProjectCommand { Id = Guid.NewGuid() },
                new UpdateProjectCommand { Id = Guid.NewGuid() }
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            _mockBusinessDomainDataService.Setup(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()))
                .Returns(Task.FromResult(bussinessReturn));

            _mockProjectDataService.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ProjectResponseModel()));

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
                    RecordStatus = false
                } }.AsEnumerable()));
            //Run Service
            var result = await _updateProjectCommandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.IsType<GenericResponse>(result);
            Assert.False(result.Success);
            Assert.Equal("Updated Failed! Some Master Data Is DeActivated! Please Reload The Page!", result.Message);

            //Verify
            _mockGenericDataService.Verify(x => x.GetAll(It.IsAny<string>()), Times.Once);
            _mockBusinessDomainDataService.Verify(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()), Times.Once);
            _mockProjectDataService.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mockServiceTypeDataService.Verify(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UpdateProjectCommandInactiveMMasterData_ReturnGenericResponse()
        {
            //Setup
            var bussinessReturn = new BusinessDomainResponseModel
            {
                Id = Guid.NewGuid(),
                BusinessDomainName = "Test",
                Status = true
            };
            var command = new UpdateProjectCommand
            {
                Id = Guid.NewGuid(),
                ProjectName = "Test",
                ProjectCode = "Test",
                MethodologyId = new List<Guid> { Guid.NewGuid() }
            };
            var listProjectReturn = new List<UpdateProjectCommand>
            {
                command,
                new UpdateProjectCommand { Id = Guid.NewGuid() },
                new UpdateProjectCommand { Id = Guid.NewGuid() }
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            _mockBusinessDomainDataService.Setup(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()))
                .Returns(Task.FromResult(bussinessReturn));

            _mockProjectDataService.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ProjectResponseModel()));

            _mockMethodologyDataService.Setup(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()))
                .Returns(Task.FromResult(new List<MethodologyResponseModel>() {
                new MethodologyResponseModel {
                    Id = Guid.NewGuid(),
                    RecordStatus = false
                } }.AsEnumerable()));
            //Run Service
            var result = await _updateProjectCommandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.IsType<GenericResponse>(result);
            Assert.False(result.Success);
            Assert.Equal("Updated Failed! Some Master Data Is DeActivated! Please Reload The Page!", result.Message);

            //Verify
            _mockGenericDataService.Verify(x => x.GetAll(It.IsAny<string>()), Times.Once);
            _mockBusinessDomainDataService.Verify(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()), Times.Once);
            _mockProjectDataService.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mockMethodologyDataService.Verify(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UpdateProjectCommandInactiveTMasterData_ReturnGenericResponse()
        {
            //Setup
            var bussinessReturn = new BusinessDomainResponseModel
            {
                Id = Guid.NewGuid(),
                BusinessDomainName = "Test",
                Status = true
            };
            var command = new UpdateProjectCommand
            {
                Id = Guid.NewGuid(),
                ProjectName = "Test",
                ProjectCode = "Test",
                TechnologyId = new List<Guid> { }
            };
            var listProjectReturn = new List<UpdateProjectCommand>
            {
                command,
                new UpdateProjectCommand { Id = Guid.NewGuid() },
                new UpdateProjectCommand { Id = Guid.NewGuid() }
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            _mockBusinessDomainDataService.Setup(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()))
                .Returns(Task.FromResult(bussinessReturn));

            _mockProjectDataService.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ProjectResponseModel()));

            _mockTechnologyDataService.Setup(x => x.GetTechnologyDetail(It.IsAny<GetTechnologyDetailsQuery>()))
                .Returns(Task.FromResult(new TechnologyResponseModel
                {
                    Id = Guid.NewGuid(),
                    TechnologyActive = false
                }));
            //Run Service
            var result = await _updateProjectCommandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.IsType<GenericResponse>(result);
            Assert.False(result.Success);
            Assert.Equal("Updated Failed! Some Master Data Is DeActivated! Please Reload The Page!", result.Message);

            //Verify
            _mockGenericDataService.Verify(x => x.GetAll(It.IsAny<string>()), Times.Once);
            _mockBusinessDomainDataService.Verify(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()), Times.Once);
            _mockProjectDataService.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mockTechnologyDataService.Verify(x => x.GetTechnologyDetail(It.IsAny<GetTechnologyDetailsQuery>()), Times.Once);
        }

    }
}
