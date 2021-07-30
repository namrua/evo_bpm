using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.ProjectRisk.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using Xunit;
using Moq;
using CleanEvoBPM.Application.CQRS.ProjectRisk.QueryHandler;
using System.Linq;
using CleanEvoBPM.Application.Models.ProjectRisk;

namespace CleanEvoBPM.Test.Application.CQRS.ProjectRisk.QueryHandler
{

    public class ProjectRiskQueryHandlerTest
    {
        private Mock<IProjectRiskDataService> _mockProjectRiskDataService;
        private FetchProjectQueryHandler _fetchProjectRiskQueryHandler;

        public ProjectRiskQueryHandlerTest()
        {
            _mockProjectRiskDataService = new Mock<IProjectRiskDataService>();
            _fetchProjectRiskQueryHandler = new FetchProjectQueryHandler(_mockProjectRiskDataService.Object);
        }

        [Fact]
        public async Task Handle_FetchProjectRiskQueryHandlerSuccess_ReturnTrue()
        {
            var projectRiskId = Guid.NewGuid();
            var projectRiskResponseModels = new List<ProjectRiskModel>
            {
                new ProjectRiskModel()
                {
                    Id = projectRiskId,
                    Name = "MD1"
                }
            }.AsEnumerable();

            _mockProjectRiskDataService.Setup(x => x.GetProjectRisks(It.IsAny<Guid>())).Returns(Task.FromResult(projectRiskResponseModels));
            var result = await _fetchProjectRiskQueryHandler.Handle(new FetchProjectRiskQuery(), new CancellationToken());
            Assert.Equal(projectRiskResponseModels.Count(), result.Count());
            Assert.Equal(projectRiskResponseModels.FirstOrDefault().Id, projectRiskId);
        }

    }
}
