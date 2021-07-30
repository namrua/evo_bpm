using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.ProjectType.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using Xunit;
using Moq;
using CleanEvoBPM.Application.CQRS.ProjectType.QueryHandler;
using CleanEvoBPM.Application.Models.ProjectType;
using System.Linq;

namespace CleanEvoBPM.Test.Application.CQRS.ProjectType.QueryHandler
{

    public class ProjectTypeQueryHandlerTest
    {
        private Mock<IProjectTypeDataService> _mockProjectTypeDataService;
        private GetProjectTypeQueryHandler _fetchProjectTypeQueryHandler;
        private GetProjectTypeDetailsHandler _getProjectTypeDetailsHandler;

        public ProjectTypeQueryHandlerTest()
        {
            _mockProjectTypeDataService = new Mock<IProjectTypeDataService>();
            _fetchProjectTypeQueryHandler = new GetProjectTypeQueryHandler(_mockProjectTypeDataService.Object);
            _getProjectTypeDetailsHandler = new GetProjectTypeDetailsHandler(_mockProjectTypeDataService.Object);
        }

        [Fact]
        public async Task Handle_FetchProjectTypeQueryHandlerSuccess_ReturnTrue()
        {
            var projectTypeId = Guid.NewGuid();
            var projectTypeResponseModels = new List<ProjectTypeResponseModel>
            {
                new ProjectTypeResponseModel{Id= projectTypeId, ProjectTypeName="Microservice"},
                new ProjectTypeResponseModel{Id=Guid.NewGuid(), ProjectTypeName="Docker"}
            }.AsEnumerable();

            _mockProjectTypeDataService.Setup(x => x.FetchProjectType(It.IsAny<GetProjectTypeQuery>())).Returns(Task.FromResult(projectTypeResponseModels));
            var result = await _fetchProjectTypeQueryHandler.Handle(new GetProjectTypeQuery(), new CancellationToken());
            Assert.Equal(projectTypeResponseModels.Count(), result.Count());
            Assert.Equal(projectTypeResponseModels.FirstOrDefault().Id, projectTypeId);
        }

        [Fact]
        public async Task Handle_GetProjectTypeDetailsHandlerSuccess_ReturnTrue()
        {
            var projectTypeId = Guid.NewGuid();
            var projectTypeResponseModel = new ProjectTypeResponseModel { Id = projectTypeId, ProjectTypeName = "Microservice" };
            _mockProjectTypeDataService.Setup(x => x.GetProjectTypeDetail(It.IsAny<GetProjectTypeDetailsQuery>())).Returns(Task.FromResult(projectTypeResponseModel));
            var result = await _getProjectTypeDetailsHandler.Handle(new GetProjectTypeDetailsQuery(), new CancellationToken());
            Assert.NotNull(result);
            Assert.Equal(projectTypeId, result.Id);
        }
    }
}
