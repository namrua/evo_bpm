using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProjectType.Command;
using CleanEvoBPM.Application.CQRS.ProjectType.Query;
using CleanEvoBPM.Application.CQRS.ProjectType.QueryHandler;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectType;
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
    public class ProjectTypeControllerTest : BaseControllerTest
    {
        private ProjectTypeController _projectTypeController;

        public ProjectTypeControllerTest() : base(new ProjectTypeController(null))
        {
            _projectTypeController = (ProjectTypeController)_apiController;
        }

        [Fact]
        public async Task Post_Success_Return201()
        {
            var command = new CreateProjectTypeCommand();
             var data = new CleanEvoBPM.Application.Common.GenericResponse(){
                Code = 201
            };
            _mockMediator.Setup(x => x.Send(command, new CancellationToken())).Returns(Task.FromResult(data));
            var result = await _projectTypeController.Post(command);
            Assert.Equal(201,result.Value.Code);
        }

        [Fact]
        public async Task GetByQuery_Success_ReturnTrue()
        {
            var  _mockProjectTypeDataService = new Mock<IProjectTypeDataService>();
            var query =new GetProjectTypeQuery();
            var id = Guid.NewGuid();
            var responseModels = new List<ProjectTypeResponseModel>{
                new ProjectTypeResponseModel { Id = id, ProjectTypeName="Java"}
            }.AsEnumerable();
            
            _mockMediator.Setup(x => x.Send(query, new CancellationToken())).Returns(Task.FromResult(responseModels));
            var result = await _projectTypeController.Get(query);
            Assert.Single(result);
            Assert.Equal(id, result.FirstOrDefault().Id);
        }

        [Fact]
        public async Task GetById_Success_ReturnTrue()
        {
            var id = Guid.NewGuid();
            var resposneModel = new ProjectTypeResponseModel { Id = id, ProjectTypeName = "NET" };
            _mockMediator.Setup(x => x.Send(It.IsAny<GetProjectTypeDetailsQuery>(), new CancellationToken())).Returns(Task.FromResult(resposneModel));
            var result = await _projectTypeController.Get(id);
            Assert.Equal(id,result.Value.Id);
            Assert.Equal("NET", result.Value.ProjectTypeName);
        }

        [Fact]
        public async Task Put_Success_ReturnTrue()
        {
            var response = new GenericResponse() { Success = true };
            var command = new UpdateProjectTypeCommand();
            _mockMediator.Setup(x => x.Send(command, new CancellationToken())).Returns(Task.FromResult(response));
            var result = await _projectTypeController.Put(Guid.NewGuid(), command);
            Assert.True(result.Value.Success);
        }

        [Fact]
        public async Task Delete_Success_SuccessGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteProjectTypeCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = true}));
            var result = await _projectTypeController.Delete(Guid.NewGuid());
            Assert.True(result.Value.Success);
        }
         [Fact]
        public async Task Delete_DeleteMasterDataFailed_FailedGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteProjectTypeCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = false, Message = ValidateMessage.DeleteMasterDataFailed}));
            var result = await _projectTypeController.Delete(Guid.NewGuid());
            Assert.False(result.Value.Success);
            Assert.Equal(ValidateMessage.DeleteMasterDataFailed,result.Value.Message);
        }
         [Fact]
        public async Task Delete_NotFound_FailedGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteProjectTypeCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = false, Message = ValidateMessage.NotFound}));
            var result = await _projectTypeController.Delete(Guid.NewGuid());
            Assert.False(result.Value.Success);
            Assert.Equal(ValidateMessage.NotFound,result.Value.Message);
        }
    }
}
