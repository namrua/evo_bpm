using CleanEvoBPM.Application.CQRS.ProjectRisk.Query;
using CleanEvoBPM.Application.Models.ProjectRisk;
using CleanEvoBPM.Test.WebAPI.Base;
using CleanEvoBPM.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.WebAPI.Controllers
{
    public class ProjectRiskControllerTest: BaseControllerTest
    {
        private ProjectRiskController _projectRiskController;

        public ProjectRiskControllerTest() : base(new ProjectRiskController())
        {
            _projectRiskController = (ProjectRiskController)_apiController;
        }

        [Fact]
        public async Task Get_Success_ReturnTrue()
        {
            var query = new FetchProjectRiskQuery();
            var id = Guid.NewGuid();
            var responseModels = new List<ProjectRiskModel>{
                new ProjectRiskModel { Id = id, Name="Pr"}
            }.AsEnumerable();
            _mockMediator.Setup(x => x.Send(query, new CancellationToken())).Returns(Task.FromResult(responseModels));
            var result = await _projectRiskController.Get(query);
            Assert.Single(result);
            Assert.Equal(id, result.FirstOrDefault().Id);
        }
    }
}
