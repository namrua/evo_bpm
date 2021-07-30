using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Command;
using CleanEvoBPM.Application.CQRS.BusinessDomain.CommandHandler;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Event;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectBusinessDomain;
using CleanEvoBPM.Domain;
using Moq;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.BusinessDomain.CommandHandler
{
    public class BusinessDomainCommandHandlerTest
    {
        private Mock<IBusinessDomainDataService> _mockBusinessDomainDataService;
        private Mock<INotificationDispatcher> _mockNotificationDispatcher;
        private CreateBusinessDomainCommandHandler _createBusinessDomainCommandHandler;
        private DeleteBusinessDomainCommandHandler _deleteBusinessDomainCommandHandler;
        private UpdateBusinessDomainCommandHandler _updateBusinessDomainCommandHandler;
        private Mock<IGenericDataService<ProjectBusinessDomainModel>> _mockGenericDataService;
        public BusinessDomainCommandHandlerTest()
        {
            _mockBusinessDomainDataService = new Mock<IBusinessDomainDataService>();
            _mockGenericDataService = new Mock<IGenericDataService<ProjectBusinessDomainModel>>();
            _mockNotificationDispatcher = new Mock<INotificationDispatcher>();
            _createBusinessDomainCommandHandler = new CreateBusinessDomainCommandHandler(_mockBusinessDomainDataService.Object,
                _mockNotificationDispatcher.Object);
            _deleteBusinessDomainCommandHandler = new DeleteBusinessDomainCommandHandler(_mockBusinessDomainDataService.Object,
            _mockGenericDataService.Object,
                _mockNotificationDispatcher.Object);
            _updateBusinessDomainCommandHandler = new UpdateBusinessDomainCommandHandler(_mockBusinessDomainDataService.Object,
                _mockNotificationDispatcher.Object);
        }

        [Fact]
        public async Task Handle_CreateBusinessDomainCommandSuccess_ReturnGennericResponse()
        {
            _mockBusinessDomainDataService.Setup(x => x.CreatBusinessDomain(It.IsAny<CreateBusinessDomainCommand>()))
                                            .Returns(Task.FromResult(new GenericResponse() { Code = 201, Success = true }));
            var result = await _createBusinessDomainCommandHandler.Handle(new CreateBusinessDomainCommand(), new CancellationToken());
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Handle_UpdateBusinessDomainCommandSuccess_ReturnTrue()
        {
            _mockBusinessDomainDataService.Setup(x => x.UpdateBusinessDomain(It.IsAny<UpdateBusinessDomainCommand>()))
                                            .Returns(Task.FromResult(true));
            var result = await _updateBusinessDomainCommandHandler.Handle(new UpdateBusinessDomainCommand(), new CancellationToken());
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_UpdateBusinessDomainCommandFailed_Returnfalse()
        {
            _mockBusinessDomainDataService.Setup(x => x.UpdateBusinessDomain(It.IsAny<UpdateBusinessDomainCommand>()))
                                            .Returns(Task.FromResult(false));
            var result = await _updateBusinessDomainCommandHandler.Handle(new UpdateBusinessDomainCommand(), new CancellationToken());
            Assert.False(result);
        }

        [Fact]
        public async Task HandleDeleteBusinessDomain_DeleteMasterDataFailed_FailedGenericResponse()
        {
            var businessDomainId = Guid.NewGuid();
            var projectBusinessDomains = new List<ProjectBusinessDomainModel>
            {
                new ProjectBusinessDomainModel
                {
                    BusinessDomainId = businessDomainId,
                    ProjectId = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteBusinessDomainCommand
            {
                Id = businessDomainId
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.ProjectBusinessDomain)).Returns(Task.FromResult(projectBusinessDomains));
            // _mockBusinessDomainDataService.Setup(x => x.DeleteBusinessDomain(It.IsAny<DeleteBusinessDomainCommand>()))
            //                                 .Returns(Task.FromResult(true));

            var result = await _deleteBusinessDomainCommandHandler.Handle(request, new CancellationToken());
            Assert.False(result.Success);
            Assert.Equal(ValidateMessage.DeleteMasterDataFailed, result.Message);
        }

        [Fact]
        public async Task HandleDeleteBusinessDomain_NotFound_FailedGenericResponse()
        {
            var businessDomainId = Guid.NewGuid();
            var projectBusinessDomains = new List<ProjectBusinessDomainModel>
            {
                new ProjectBusinessDomainModel
                {
                    BusinessDomainId = businessDomainId,
                    ProjectId = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteBusinessDomainCommand
            {
                Id = Guid.NewGuid()
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.ProjectBusinessDomain)).Returns(Task.FromResult(projectBusinessDomains));
            // _mockBusinessDomainDataService.Setup(x => x.DeleteBusinessDomain(It.IsAny<DeleteBusinessDomainCommand>()))
            //                                 .Returns(Task.FromResult(true));

            var result = await _deleteBusinessDomainCommandHandler.Handle(request, new CancellationToken());
            Assert.False(result.Success);
            Assert.Equal(ValidateMessage.NotFound, result.Message);
        }
        [Fact]
        public async Task HandleDeleteBusinessDomain_Success_SuccessGenericResponse()
        {
            var businessDomainId = Guid.NewGuid();
            var projectBusinessDomains = new List<ProjectBusinessDomainModel>
            {
                new ProjectBusinessDomainModel
                {
                    BusinessDomainId = businessDomainId,
                    ProjectId = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteBusinessDomainCommand
            {
                Id = Guid.NewGuid()
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.ProjectBusinessDomain)).Returns(Task.FromResult(projectBusinessDomains));
            _mockBusinessDomainDataService.Setup(x => x.DeleteBusinessDomain(It.IsAny<DeleteBusinessDomainCommand>()))
                                            .Returns(Task.FromResult(true));

            var result = await _deleteBusinessDomainCommandHandler.Handle(request, new CancellationToken());
            Assert.True(result.Success);
            Assert.Equal(ValidateMessage.DeleteSucess, result.Message);
        }
    }
}