using CleanEvoBPM.Application.CQRS.Project.Query;
using CleanEvoBPM.Application.CQRS.Project.QueryHandler;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Project;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.Project.QueryHandler
{
    public class FetchProjectQueryHandlerTest
    {
        private readonly Mock<IProjectDataService> _mockProjectDataService;
        private readonly Mock<IBusinessDomainDataService> _mockBusinessDomainDataService;
        private readonly Mock<IClientDataService> _mockClientDataService;
        private readonly FetchProjectQueryHandler _fetchProjectQueryHandler;

        public FetchProjectQueryHandlerTest()
        {
            _mockProjectDataService = new Mock<IProjectDataService>();
            _mockBusinessDomainDataService = new Mock<IBusinessDomainDataService>();
            _mockClientDataService = new Mock<IClientDataService>();
            _fetchProjectQueryHandler = new FetchProjectQueryHandler(
                _mockProjectDataService.Object,
                _mockBusinessDomainDataService.Object,
                _mockClientDataService.Object);
        }

        [Fact]
        public async Task Handle_Success_ReturnEnumerable()
        {
            //Setup
            var projectReturn = new List<ProjectResponseModel>
            {
                new ProjectResponseModel()
            };
            var request = new FetchProjectQuery();

            //Mock
            _mockProjectDataService.Setup(x => x.FetchProject(request))
                .Returns(Task.FromResult(projectReturn.AsEnumerable()));

            //Run Service
            var result = await _fetchProjectQueryHandler.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotEmpty(result);
            Assert.Single(result);

            //Verify
            _mockProjectDataService.Verify(x => x.FetchProject(request), Times.Once);
        }
    }
}
