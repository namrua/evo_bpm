using CleanEvoBPM.Application.CQRS.ProjectMilestone.Query;
using CleanEvoBPM.Application.Models.ProjectMilestone;
using CleanEvoBPM.Test.WebAPI.Base;
using CleanEvoBPM.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.WebAPI.Controllers
{
    public class ProjectMilestoneControllerTest : BaseControllerTest
    {
        private ProjectMilestoneController _projectMilestoneController;

        public ProjectMilestoneControllerTest() : base(new ProjectMilestoneController())
        {
            _projectMilestoneController = (ProjectMilestoneController)_apiController;
        }


        [Fact]
        public async Task GetByQuery_Success_ReturnTrue()
        {
            var query = new FetchProjectMilestoneQuery();
            var id = Guid.NewGuid();
            var responseModels = new List<ProjectMilestoneModel>{
                new ProjectMilestoneModel { Id = id, Description="Java", Date = DateTime.Now}
            }.AsEnumerable();
            _mockMediator.Setup(x => x.Send(query, new CancellationToken())).Returns(Task.FromResult(responseModels));
            var result = await _projectMilestoneController.Get(query);
            Assert.Single(result);
            Assert.Equal(id, result.FirstOrDefault().Id);
        }

    }
}
