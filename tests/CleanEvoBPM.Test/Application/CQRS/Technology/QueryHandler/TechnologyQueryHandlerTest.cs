using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.Technology.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using Xunit;
using Moq;
using CleanEvoBPM.Application.CQRS.Technology.QueryHandler;
using CleanEvoBPM.Application.Models.Technology;
using System.Linq;

namespace CleanEvoBPM.Test.Application.CQRS.Technology.QueryHandler
{

    public class TechnologyQueryHandlerTest
    {
        private Mock<ITechnologyDataService> _mockTechnologyDataService;
        private FetchTechnologyQueryHandler _fetchTechnologyQueryHandler;
        private GetTechnologyDetailsHandler _getTechnologyDetailsHandler;

        public TechnologyQueryHandlerTest()
        {
            _mockTechnologyDataService = new Mock<ITechnologyDataService>();
            _fetchTechnologyQueryHandler = new FetchTechnologyQueryHandler(_mockTechnologyDataService.Object);
            _getTechnologyDetailsHandler = new GetTechnologyDetailsHandler(_mockTechnologyDataService.Object);
        }

        [Fact]
        public async Task Handle_FetchTechnologyQueryHandlerSuccess_ReturnTrue()
        {
            var technologyId = Guid.NewGuid();
            var technologyResponseModels = new List<TechnologyResponseModel>
            {
                new TechnologyResponseModel{Id=technologyId, TechnologyName="Microservice"},
                new TechnologyResponseModel{Id=Guid.NewGuid(), TechnologyName="Docker"}
            }.AsEnumerable();

            _mockTechnologyDataService.Setup(x => x.Fetch(It.IsAny<FetchTechnologyQuery>())).Returns(Task.FromResult(technologyResponseModels));
            var result = await _fetchTechnologyQueryHandler.Handle(new FetchTechnologyQuery(), new CancellationToken());
            Assert.Equal(technologyResponseModels.Count(), result.Count());
            Assert.Equal(technologyResponseModels.FirstOrDefault().Id, technologyId);
        }

        [Fact]
        public async Task Handle_GetTechnologyDetailsHandlerSuccess_ReturnTrue()
        {
            var technologyId = Guid.NewGuid();
            var technologyResponseModel = new TechnologyResponseModel { Id = technologyId, TechnologyName = "Microservice" };
            _mockTechnologyDataService.Setup(x => x.GetTechnologyDetail(It.IsAny<GetTechnologyDetailsQuery>())).Returns(Task.FromResult(technologyResponseModel));
            var result = await _getTechnologyDetailsHandler.Handle(new GetTechnologyDetailsQuery(), new CancellationToken());
            Assert.NotNull(result);
            Assert.Equal(technologyId, result.Id);
        }
    }
}
