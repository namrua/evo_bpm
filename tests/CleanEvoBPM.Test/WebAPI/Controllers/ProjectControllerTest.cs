using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Project.Command;
using CleanEvoBPM.Application.CQRS.Project.Query;
using CleanEvoBPM.Application.CQRS.Resource.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Project;
using CleanEvoBPM.Test.WebAPI.Base;
using CleanEvoBPM.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.WebAPI.Controllers
{
    public class ProjectControllerTest : BaseControllerTest
    {
        private ProjectController _projectController;
        public ProjectControllerTest() : base(new ProjectController(null))
        {
            _projectController = (ProjectController)_apiController;
        }

        [Fact]
        public async Task Post_Success_ReturnGenericResponse()
        {
            //Setup
            var successResult = new GenericResponse
            {
                Success = true,
                Code = 200,
                Message = "Create Project Successfully!"
            };
            var command = new CreateProjectCommand();

            //Mock
            _mockMediator.Setup(x => x.Send(command, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(successResult));

            //Run Service
            var result = await _projectController.Post(command);

            //Assert
            Assert.IsType<GenericResponse>(result.Value);
            Assert.True(result.Value.Success);
            Assert.Equal(result.Value.Message, successResult.Message);

            //Verify
            _mockMediator.Verify(x => x.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetAll_Success_ReturnEnumerable()
        {

            //Setup
            var successResult = new List<ProjectResponseModel>()
            {
                new ProjectResponseModel
                {
                    ProjectName = "Test",
                    ProjectCode = "Test"
                },
                new ProjectResponseModel
                {
                    ProjectName = "Test2",
                    ProjectCode = "Test2"
                }
            };

            //Mock
            _mockMediator.Setup(x => x.Send(It.IsAny<FetchProjectQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(successResult.AsEnumerable()));

            //Run Service
            var result = await _projectController.Get(new FetchProjectQuery());

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());

            //Verify
            _mockMediator.Verify(x => x.Send(It.IsAny<FetchProjectQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetById_Success_ReturnProjectResponseModel()
        {

            //Setup
            var id = Guid.NewGuid();
            var successResult = new ProjectResponseByIdModel
            {
                Id = id,
                ProjectName = "Test",
                ProjectCode = "Test"
            };
            var response = new GenericResponse<ProjectResponseByIdModel>() { Success = true, Content = successResult};

            //Mock
            _mockMediator.Setup(x => x.Send(It.IsAny<GetProjectById>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(response));

            //Run Service
            var result = await _projectController.Get(id);

            //Assert
            Assert.Equal(id, result.Content.Id);
            Assert.Equal(successResult.ProjectCode, result.Content.ProjectCode);

            //Verify
            _mockMediator.Verify(x => x.Send(It.IsAny<GetProjectById>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Put_Success_ReturnGenericResponse()
        {
            //Setup
            var id = Guid.NewGuid();
            var successResult = new GenericResponse
            {
                Success = true,
                Code = 200,
                Message = "Updated Successfully!"
            };
            var command = new UpdateProjectCommand();
            //Mock
            _mockMediator.Setup(x => x.Send(command, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(successResult));

            //Run Service
            var result = await _projectController.Put(id, command);

            //Assert
            Assert.IsType<GenericResponse>(result.Value);
            Assert.True(result.Value.Success);
            Assert.Equal(result.Value.Message, successResult.Message);

            //Verify
            _mockMediator.Verify(x => x.Send(It.IsAny<UpdateProjectCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Get_MyProject_ReturnsNull()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }, "mock"));

            _projectController.HttpContext.User = user;

            _mockMediator
                .Setup(x => x.Send(It.IsAny<GetProjectsByManagerQuery>(), It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(null as IEnumerable<ProjectResponseModel>));

            var result = await _projectController.GetMyProjects();

            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(GetProjectsTheoryData))]
        public async Task Get_MyProject_ReturnsList(IEnumerable<ProjectResponseModel> model)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }, "mock"));

            _projectController.HttpContext.User = user;

            _mockMediator
                .Setup(x => x.Send(It.IsAny<GetProjectsByManagerQuery>(), It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(model));

            var result = await _projectController.GetMyProjects();

            Assert.Equal(result, model);
        }

        [Fact]
        public async Task Get_MyProjectById_ReturnsEmpty()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }, "mock"));

            _projectController.HttpContext.User = user;

            _mockMediator
                .Setup(x => x.Send(It.IsAny<GetMyProjectByIdQuery>(), It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(null as GenericResponse<ProjectResponseModel>));

            var result = await _projectController.GetMyProjectById(Guid.NewGuid());

            Assert.True(result is EmptyResult);
        }

        [Fact]
        public async Task Get_MyProjectById_Success()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }, "mock"));

            _projectController.HttpContext.User = user;

            _mockMediator
                .Setup(x => x.Send(It.IsAny<GetMyProjectByIdQuery>(), It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(new GenericResponse<ProjectResponseModel> { Code = 200 }));

            var result = await _projectController.GetMyProjectById(Guid.NewGuid());

            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public async Task GetProjectStatus_Success()
        {
            _mockMediator
                .Setup(x => x.Send(It.IsAny<GetProjectCountByStatusQuery>(), It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(new List<ProjectCountByStatusModel>() as IEnumerable<ProjectCountByStatusModel>));

            var result = await _projectController.GetProjectCountByStatus();

            Assert.True(result is IEnumerable<ProjectCountByStatusModel>);
        }

        public static TheoryData<IEnumerable<ProjectResponseModel>> GetProjectsTheoryData
        {
            get
            {
                return new TheoryData<IEnumerable<ProjectResponseModel>>
                {
                    new List<ProjectResponseModel>
                    {
                        new ProjectResponseModel(),
                        new ProjectResponseModel()
                    },
                    new List<ProjectResponseModel>()
                };
            }
        }
    }
}
