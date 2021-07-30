using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Technology.Command;
using CleanEvoBPM.Application.CQRS.Technology.CommandHandler;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectTechnology;
using CleanEvoBPM.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.Technology.CommandHandler
{
    public class TechnologyCommandHandlerTest
    {
        private Mock<ITechnologyDataService> _mockTechnologyDataService;
        private CreateTechnologyCommandHandler _createTechnologyCommandHandler;
        private UpdateTechnologyCommandHandler _updateTechnologyCommandHandler;
        private DeleteTechnologyCommandHandler _deleteTechnologyCommandHandler;
        private Mock<IGenericDataService<ProjectTechnologyModel>> _mockGenericDataService;
        private readonly Mock<INotificationDispatcher> _mockNotificationDispatcher;
        public TechnologyCommandHandlerTest()
        {
            _mockTechnologyDataService = new Mock<ITechnologyDataService>();
            _mockNotificationDispatcher = new Mock<INotificationDispatcher>();
            _mockGenericDataService = new Mock<IGenericDataService<ProjectTechnologyModel>>();
            _createTechnologyCommandHandler = new CreateTechnologyCommandHandler(_mockTechnologyDataService.Object,
                _mockNotificationDispatcher.Object);
            _updateTechnologyCommandHandler = new UpdateTechnologyCommandHandler(_mockTechnologyDataService.Object,
                _mockNotificationDispatcher.Object);
            _deleteTechnologyCommandHandler = new DeleteTechnologyCommandHandler(_mockTechnologyDataService.Object,
                _mockGenericDataService.Object, _mockNotificationDispatcher.Object);
        }
        [Fact]
        public async Task Handle_CreateTechnologyCommandSuccess_ReturnTrue()
        {
            var response = new GenericResponse() { Success = true };
            _mockTechnologyDataService.Setup(x => x.Create(It.IsAny<CreateTechnologyCommand>())).Returns(Task.FromResult(response));
            var result = await _createTechnologyCommandHandler.Handle(new CreateTechnologyCommand(), new CancellationToken());
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Handle_UpdateTechnologyCommandSuccess_ReturnTrue()
        {
            var response = new GenericResponse() { Success = true };
            _mockTechnologyDataService.Setup(x => x.Update(It.IsAny<UpdateTechnologyCommand>())).Returns(Task.FromResult(response));
            var result = await _updateTechnologyCommandHandler.Handle(new UpdateTechnologyCommand(), new CancellationToken());
            Assert.True(result.Success);
        }
        [Fact]
        public async Task HandleDeleteTechnology_DeleteMasterDataFailed_FailedGenericResponse()
        {
            var techId = Guid.NewGuid();
            var projectTechs = new List<ProjectTechnologyModel>
            {
                new ProjectTechnologyModel
                {
                    TechnologyId = techId,
                    ProjectId = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteTechnologyCommand
            {
                Id = techId
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.ProjectTechnology))
            .Returns(Task.FromResult(projectTechs));

            var result = await _deleteTechnologyCommandHandler.Handle(request, new CancellationToken());
            Assert.False(result.Success);
            Assert.Equal(ValidateMessage.DeleteMasterDataFailed, result.Message);
        }

        [Fact]
        public async Task HandleDeleteTechnology_NotFound_FailedGenericResponse()
        {
            var techId = Guid.NewGuid();
            var projectBusinessDomains = new List<ProjectTechnologyModel>
            {
                new ProjectTechnologyModel
                {
                    TechnologyId = techId,
                    ProjectId = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteTechnologyCommand
            {
                Id = Guid.NewGuid()
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.ProjectBusinessDomain)).Returns(Task.FromResult(projectBusinessDomains));

            var result = await _deleteTechnologyCommandHandler.Handle(request, new CancellationToken());
            Assert.False(result.Success);
            Assert.Equal(ValidateMessage.NotFound, result.Message);
        }
        [Fact]
        public async Task HandleDeleteTechnology_Success_SuccessGenericResponse()
        {
            var techId = Guid.NewGuid();
            var projectBusinessDomains = new List<ProjectTechnologyModel>
            {
                new ProjectTechnologyModel
                {
                    TechnologyId = techId,
                    ProjectId = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteTechnologyCommand
            {
                Id = Guid.NewGuid()
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.ProjectBusinessDomain)).Returns(Task.FromResult(projectBusinessDomains));
            _mockTechnologyDataService.Setup(x => x.Delete(It.IsAny<Guid>()))
                                            .Returns(Task.FromResult(true));

            var result = await _deleteTechnologyCommandHandler.Handle(request, new CancellationToken());
            Assert.True(result.Success);
            Assert.Equal(ValidateMessage.DeleteSucess, result.Message);
        }
    }
}
