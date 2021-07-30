using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Technology.Command;
using CleanEvoBPM.Application.CQRS.Technology.Query;
using CleanEvoBPM.Application.Models.Technology;
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
    public class TechnologyControllerTest : BaseControllerTest
    {
        private TechnologyController _technologyController;

        public TechnologyControllerTest() : base(new TechnologyController(null))
        {
            _technologyController = (TechnologyController)_apiController;
        }

        [Fact]
        public async Task Post_Success_ReturnTrue()
        {
            var response = new GenericResponse() { Success = true };
            var command = new CreateTechnologyCommand();
            _mockMediator.Setup(x => x.Send(command, new CancellationToken())).Returns(Task.FromResult(response));
            var result = await _technologyController.Post(command);
            Assert.True(result.Value.Success);
        }

        [Fact]
        public async Task GetByQuery_Success_ReturnTrue()
        {
            var query = new FetchTechnologyQuery();
            var id = Guid.NewGuid();
            var responseModels = new List<TechnologyResponseModel>{
                new TechnologyResponseModel { Id = id, TechnologyName="Java"}
            }.AsEnumerable();
            _mockMediator.Setup(x => x.Send(query, new CancellationToken())).Returns(Task.FromResult(responseModels));
            var result = await _technologyController.Get(query);
            Assert.Single(result);
            Assert.Equal(id, result.FirstOrDefault().Id);
        }

        [Fact]
        public async Task GetById_Success_ReturnTrue()
        {
            var id = Guid.NewGuid();
            var resposneModel = new TechnologyResponseModel { Id = id, TechnologyName = "NET" };
            _mockMediator.Setup(x => x.Send(It.IsAny<GetTechnologyDetailsQuery>(), new CancellationToken())).Returns(Task.FromResult(resposneModel));
            var result = await _technologyController.Get(id);
            Assert.Equal(id,result.Value.Id);
            Assert.Equal("NET", result.Value.TechnologyName);
        }

        [Fact]
        public async Task Put_Success_ReturnTrue()
        {
            var response = new GenericResponse() { Success = true };
            var command = new UpdateTechnologyCommand();
            _mockMediator.Setup(x => x.Send(command, new CancellationToken())).Returns(Task.FromResult(response));
            var result = await _technologyController.Put(Guid.NewGuid(), command);
            Assert.True(result.Value.Success);
        }

        [Fact]
        public async Task Delete_Success_SuccessGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteTechnologyCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = true}));
            var result = await _technologyController.Delete(Guid.NewGuid());
            Assert.True(result.Value.Success);
        }
         [Fact]
        public async Task Delete_DeleteMasterDataFailed_FailedGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteTechnologyCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = false, Message = ValidateMessage.DeleteMasterDataFailed}));
            var result = await _technologyController.Delete(Guid.NewGuid());
            Assert.False(result.Value.Success);
            Assert.Equal(ValidateMessage.DeleteMasterDataFailed,result.Value.Message);
        }
         [Fact]
        public async Task Delete_NotFound_FailedGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteTechnologyCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = false, Message = ValidateMessage.NotFound}));
            var result = await _technologyController.Delete(Guid.NewGuid());
            Assert.False(result.Value.Success);
            Assert.Equal(ValidateMessage.NotFound,result.Value.Message);
        }
    }
}
