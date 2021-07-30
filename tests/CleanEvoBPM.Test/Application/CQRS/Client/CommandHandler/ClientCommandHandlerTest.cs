using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Client.Command;
using CleanEvoBPM.Application.CQRS.Client.CommandHandler;
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

namespace CleanEvoBPM.Test.Application.CQRS.Client.CommandHandler
{
    public class ClientCommandHandlerTest
    {
        private Mock<IClientDataService> _mockClientDataService;
        private Mock<INotificationDispatcher> _mockNotificationDispatcher;
        private CreateClientCommandHandler _createClientCommandHandler;
        private UpdateClientCommandHandler _updateClientCommandHandler;
        private DeleteClientCommandHandler _deleteClientCommandHandler;
        private Mock<IGenericDataService<ProjectMasterDataToDelete>> _mockGenericDataService;

        public ClientCommandHandlerTest()
        {
            _mockClientDataService = new Mock<IClientDataService>();
            _mockGenericDataService = new Mock<IGenericDataService<ProjectMasterDataToDelete>>();
            _mockNotificationDispatcher = new Mock<INotificationDispatcher>();
            _createClientCommandHandler = new CreateClientCommandHandler(_mockClientDataService.Object, _mockNotificationDispatcher.Object);
            _updateClientCommandHandler = new UpdateClientCommandHandler(_mockClientDataService.Object, _mockNotificationDispatcher.Object);
            _deleteClientCommandHandler = new DeleteClientCommandHandler(_mockClientDataService.Object, _mockGenericDataService.Object,_mockNotificationDispatcher.Object);
        }
        [Fact]
        public async Task Handle_CreateClientCommandSuccess_Return201()
        {
            var data =new GenericResponse<CleanEvoBPM.Application.Models.Client.ClientResponseModel>(){
                Code = 201
            };

            _mockClientDataService.Setup(x => x.CreateClient(It.IsAny<CreateClientCommand>())).Returns(Task.FromResult(data) );

            var result = await _createClientCommandHandler.Handle(new CreateClientCommand(), new CancellationToken());
            Assert.Equal(201,result.Code);
        }

        [Fact]
        public async Task Handle_UpdateClientCommandSuccess_ReturnFalse()
        {
            var response = new GenericResponse() { Success = true };
            _mockClientDataService.Setup(x => x.UpdateClient(It.IsAny<UpdateClientCommand>())).Returns(Task.FromResult(response));
            var result = await _updateClientCommandHandler.Handle(new UpdateClientCommand(), new CancellationToken());
            Assert.True(result.Success);
        }
        [Fact]
        public async Task HandleDeleteClient_DeleteMasterDataFailed_FailedGenericResponse()
        {
            var clientId = Guid.NewGuid();
            var projectMasterData = new List<ProjectMasterDataToDelete>
            {
                new ProjectMasterDataToDelete
                {
                    ClientId = clientId,
                    Id = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteClientCommand
            {
                Id = clientId
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.Project)).Returns(Task.FromResult(projectMasterData));

            var result = await _deleteClientCommandHandler.Handle(request, new CancellationToken());
            Assert.False(result.Success);
            Assert.Equal(ValidateMessage.DeleteMasterDataFailed, result.Message);
        }

        [Fact]
        public async Task HandleDeleteClient_NotFound_FailedGenericResponse()
        {
            var clientId = Guid.NewGuid();
            var projectMasterData = new List<ProjectMasterDataToDelete>
            {
                new ProjectMasterDataToDelete
                {
                    ClientId = clientId,
                    Id = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteClientCommand
            {
                Id = Guid.NewGuid()
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.ProjectBusinessDomain)).Returns(Task.FromResult(projectMasterData));

            var result = await _deleteClientCommandHandler.Handle(request, new CancellationToken());
            Assert.False(result.Success);
            Assert.Equal(ValidateMessage.NotFound, result.Message);
        }
        [Fact]
        public async Task HandleDeleteClient_Success_SuccessGenericResponse()
        {
            var businessDomainId = Guid.NewGuid();
            var projectMasterData = new List<ProjectMasterDataToDelete>
            {
                new ProjectMasterDataToDelete
                {
                    ClientId = businessDomainId,
                    Id = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteClientCommand
            {
                Id = Guid.NewGuid()
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.ProjectBusinessDomain)).Returns(Task.FromResult(projectMasterData));
            _mockClientDataService.Setup(x => x.DeleteClient(It.IsAny<DeleteClientCommand>()))
                                            .Returns(Task.FromResult(true));

            var result = await _deleteClientCommandHandler.Handle(request, new CancellationToken());
            Assert.True(result.Success);
            Assert.Equal(ValidateMessage.DeleteSucess, result.Message);
        }
    }
}
