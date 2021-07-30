using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProjectType.Command;
using CleanEvoBPM.Application.CQRS.ProjectType.CommandHandler;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Project;
using CleanEvoBPM.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.ProjectType.CommandHandler
{
    public class ProjectTypeCommandHandlerTest
    {
        private Mock<IProjectTypeDataService> _mockProjectTypeDataService;
        private readonly Mock<INotificationDispatcher> _mockNotificationDispatcher;
        private CreateProjectTypeCommandHandler _createProjectTypeCommandHandler;
        private UpdateProjectTypeCommandHandler _updateProjectTypeCommandHandler;
        private DeleteProjectTypeCommandHandler _deleteProjectTypeCommandHandler;
        private Mock<IGenericDataService<ProjectMasterDataToDelete>> _mockGenericDataService;

        public ProjectTypeCommandHandlerTest()
        {
            _mockProjectTypeDataService = new Mock<IProjectTypeDataService>();
             _mockGenericDataService = new Mock<IGenericDataService<ProjectMasterDataToDelete>>();
            _mockNotificationDispatcher = new Mock<INotificationDispatcher>();
            _createProjectTypeCommandHandler = new CreateProjectTypeCommandHandler(_mockProjectTypeDataService.Object,
                _mockNotificationDispatcher.Object);
            _updateProjectTypeCommandHandler = new UpdateProjectTypeCommandHandler(_mockProjectTypeDataService.Object,
                _mockNotificationDispatcher.Object);
            _deleteProjectTypeCommandHandler = new DeleteProjectTypeCommandHandler(_mockProjectTypeDataService.Object,
                _mockGenericDataService.Object,
                _mockNotificationDispatcher.Object);
        }
        [Fact]
        public async Task Handle_CreateProjectTypeCommandSuccess_Return201()
        {
            var data = new CleanEvoBPM.Application.Common.GenericResponse(){
                Code = 201
            };
            _mockProjectTypeDataService.Setup(x => x.CreateProjectType(It.IsAny<CreateProjectTypeCommand>())).Returns(Task.FromResult(data));
            var result = await _createProjectTypeCommandHandler.Handle(new CreateProjectTypeCommand(), new CancellationToken());
            Assert.Equal(201,result.Code);
        }

        [Fact]
        public async Task Handle_UpdateProjectTypeCommandSuccess_ReturnTrue()
        {
            var response = new GenericResponse() { Success = true };
            _mockProjectTypeDataService.Setup(x => x.UpdateProjectType(It.IsAny<UpdateProjectTypeCommand>())).Returns(Task.FromResult(response));
            var result = await _updateProjectTypeCommandHandler.Handle(new UpdateProjectTypeCommand(), new CancellationToken());
            Assert.True(result.Success);
        }
        [Fact]
        public async Task HandleDeleteProjectType_DeleteMasterDataFailed_FailedGenericResponse()
        {
            var projectTypeId = Guid.NewGuid();
            var projectMasterData = new List<ProjectMasterDataToDelete>
            {
                new ProjectMasterDataToDelete
                {
                    ProjectTypeId = projectTypeId,
                    Id = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteProjectTypeCommand
            {
                Id = projectTypeId
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.Project)).Returns(Task.FromResult(projectMasterData));

            var result = await _deleteProjectTypeCommandHandler.Handle(request, new CancellationToken());
            Assert.False(result.Success);
            Assert.Equal(ValidateMessage.DeleteMasterDataFailed, result.Message);
        }

        [Fact]
        public async Task HandleDeleteProjectType_NotFound_FailedGenericResponse()
        {
            var projectTypeId = Guid.NewGuid();
            var projectMasterData = new List<ProjectMasterDataToDelete>
            {
                new ProjectMasterDataToDelete
                {
                    ProjectTypeId = projectTypeId,
                    Id = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteProjectTypeCommand
            {
                Id = Guid.NewGuid()
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.ProjectBusinessDomain)).Returns(Task.FromResult(projectMasterData));

            var result = await _deleteProjectTypeCommandHandler.Handle(request, new CancellationToken());
            Assert.False(result.Success);
            Assert.Equal(ValidateMessage.NotFound, result.Message);
        }
        [Fact]
        public async Task HandleDeleteProjectType_Success_SuccessGenericResponse()
        {
            var projectTypeId = Guid.NewGuid();
            var projectMasterData = new List<ProjectMasterDataToDelete>
            {
                new ProjectMasterDataToDelete
                {
                    ProjectTypeId = projectTypeId,
                    Id = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteProjectTypeCommand
            {
                Id = Guid.NewGuid()
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.ProjectBusinessDomain)).Returns(Task.FromResult(projectMasterData));
            _mockProjectTypeDataService.Setup(x => x.DeleteProjectType(It.IsAny<Guid>()))
                                            .Returns(Task.FromResult(true));

            var result = await _deleteProjectTypeCommandHandler.Handle(request, new CancellationToken());
            Assert.True(result.Success);
            Assert.Equal(ValidateMessage.DeleteSucess, result.Message);
        }
    }
}
