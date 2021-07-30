using CleanEvoBPM.Application.CQRS.Project.Query;
using CleanEvoBPM.Application.CQRS.Project.QueryHandler;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Project;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.Project.QueryHandler
{
    public class GetProjectGroup
    {
        private readonly Mock<IProjectDataService> _mockProjectDataService;
        private readonly GetProjectCountByStatusQueryHandler _getProjectCountByStatusQueryHandler;

        public GetProjectGroup()
        {
            _mockProjectDataService = new Mock<IProjectDataService>();
            _getProjectCountByStatusQueryHandler =
                new GetProjectCountByStatusQueryHandler(_mockProjectDataService.Object, null, null);
        }

        [Fact]
        public async Task GetData_Success_ReturnEnumerable()
        {
            //Mock
            _mockProjectDataService.Setup(x => x.GetProjectCountByStatus())
                .Returns(Task.FromResult(new List<ProjectCountByStatusModel>() as IEnumerable<ProjectCountByStatusModel>));

            var request = new GetProjectCountByStatusQuery();

            //Run Service
            var result = await _getProjectCountByStatusQueryHandler.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            Assert.True(result is IEnumerable<ProjectCountByStatusModel>);
        }
    }
}
