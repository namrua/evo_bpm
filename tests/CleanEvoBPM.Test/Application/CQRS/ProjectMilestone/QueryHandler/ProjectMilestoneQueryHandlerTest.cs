using CleanEvoBPM.Application.CQRS.ProjectMilestone.Query;
using CleanEvoBPM.Application.CQRS.ProjectMilestone.QueryHandle;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectMilestone;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.ProjectMilestone.QueryHandler
{
    public class ProjectMilestoneQueryHandlerTest
    {
        private Mock<IProjectMilestoneDataService> _mockProjectMilestoneDataService;
        private FetchProjectMilestoneQueryHandle _fetchProjectMilestoneQueryHandle;

        public ProjectMilestoneQueryHandlerTest()
        {
            _mockProjectMilestoneDataService = new Mock<IProjectMilestoneDataService>();
            _fetchProjectMilestoneQueryHandle = new FetchProjectMilestoneQueryHandle(_mockProjectMilestoneDataService.Object);
        }

        [Fact]
        public async Task Handle_FetchProjectMilestoneQueryHandlerSuccess_ReturnList()
        {
            var id = Guid.NewGuid();

            var projectMilestoneModel = new List<ProjectMilestoneModel>
            {
                new ProjectMilestoneModel{Id=id, Date= DateTime.Now, Description="Description"},
                new ProjectMilestoneModel{Id=Guid.NewGuid(),Date= DateTime.Now, Description="Description2"}
            }.AsEnumerable();

            _mockProjectMilestoneDataService.Setup(x => x.GetProjectMilestones(It.IsAny<Guid>())).Returns(Task.FromResult(projectMilestoneModel));
            var result = await _fetchProjectMilestoneQueryHandle.Handle(new FetchProjectMilestoneQuery(), new CancellationToken());
            Assert.Equal(projectMilestoneModel.Count(), result.Count());
            Assert.Equal(projectMilestoneModel.FirstOrDefault().Id, id);
        }

    }
}
