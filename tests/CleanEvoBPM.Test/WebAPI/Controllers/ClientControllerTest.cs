using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Client.Command;
using CleanEvoBPM.Application.CQRS.Client.Query;
using CleanEvoBPM.Application.Models.Client;
using CleanEvoBPM.Test.WebAPI.Base;
using CleanEvoBPM.WebAPI.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.WebAPI.Test.WebAPI.Controllers
{
    public class ClientControllerTest : BaseControllerTest
    {
        private ClientController _clientController;

        public ClientControllerTest() : base(new ClientController(null))
        {
            _clientController = (ClientController)_apiController;
        }

        [Fact]
        public async Task Post_Success_Return201()
        {
            var command = new CreateClientCommand();
            var data = new CleanEvoBPM.Application.Common.GenericResponse(){
                Code = 201
            };
            _mockMediator.Setup(x => x.Send(command, new CancellationToken())).Returns(Task.FromResult(data));
            var result = await _clientController.Post(command);
            Assert.Equal(201,result.Value.Code);
        }

        [Fact]
        public async Task GetByQuery_Success_ReturnTrue()
        {
            var query = new FetchClientQuery();
            var id = Guid.NewGuid();
            var responseModels = new List<ClientResponseModel>{
                new ClientResponseModel { Id = id, ClientName="Java"}
            }.AsEnumerable();
            _mockMediator.Setup(x => x.Send(query, new CancellationToken())).Returns(Task.FromResult(responseModels));
            var result = await _clientController.Get(query);
            Assert.Single(result);
            Assert.Equal(id, result.FirstOrDefault().Id);
        }

        [Fact]
        public async Task GetById_Success_ReturnTrue()
        {
            var id = Guid.NewGuid();
            var resposneModel = new ClientResponseModel { Id = id, ClientName = "NET" };
            _mockMediator.Setup(x => x.Send(It.IsAny<GetClientDetailQuery>(), new CancellationToken())).Returns(Task.FromResult(resposneModel));
            var result = await _clientController.Get(id);
            Assert.Equal(id,result.Value.Id);
            Assert.Equal("NET", result.Value.ClientName);
        }

        [Fact]
        public async Task Put_Success_ReturnTrue()
        {
            var response = new GenericResponse() { Success = true };
            var command = new UpdateClientCommand();
            _mockMediator.Setup(x => x.Send(command, new CancellationToken())).Returns(Task.FromResult(response));
            var result = await _clientController.Put(Guid.NewGuid(), command);
            Assert.True(result.Value.Success);
        }

         [Fact]
        public async Task Delete_Success_SuccessGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteClientCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = true}));
            var result = await _clientController.Delete(Guid.NewGuid());
            Assert.True(result.Value.Success);
        }
         [Fact]
        public async Task Delete_DeleteMasterDataFailed_FailedGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteClientCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = false, Message = ValidateMessage.DeleteMasterDataFailed}));
            var result = await _clientController.Delete(Guid.NewGuid());
            Assert.False(result.Value.Success);
            Assert.Equal(ValidateMessage.DeleteMasterDataFailed,result.Value.Message);
        }
         [Fact]
        public async Task Delete_NotFound_FailedGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteClientCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = false, Message = ValidateMessage.NotFound}));
            var result = await _clientController.Delete(Guid.NewGuid());
            Assert.False(result.Value.Success);
            Assert.Equal(ValidateMessage.NotFound,result.Value.Message);
        }
    }
}
