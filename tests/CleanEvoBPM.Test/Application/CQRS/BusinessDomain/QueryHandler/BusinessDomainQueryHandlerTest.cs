using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Query;
using CleanEvoBPM.Application.CQRS.BusinessDomain.QueryHandler;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.BusinessDomain;
using Moq;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.BusinessDomain.QueryHandler
{
    public class BusinessDomainQueryHandlerTest
    {
        private Mock<IBusinessDomainDataService> _mockBusinessDomainDataService;
        private FetchBusinessDomainQueryHandler _fecthBusinessDomainQueryHandler;
        private GetBusinessDomainDetailsHandler _getBusinessDomainDetailHandler;
        public BusinessDomainQueryHandlerTest()
        {
            _mockBusinessDomainDataService = new Mock<IBusinessDomainDataService>();
            _fecthBusinessDomainQueryHandler = new FetchBusinessDomainQueryHandler(_mockBusinessDomainDataService.Object);
            _getBusinessDomainDetailHandler = new GetBusinessDomainDetailsHandler(_mockBusinessDomainDataService.Object);
        }

        [Fact]
        public async Task Handler_FetchBusinessDomainQueryHandler_ReturnIEnumerableBusinessDomainResponseModel()
        {
            var businessDomains = new List<BusinessDomainResponseModel>
            {
                new BusinessDomainResponseModel { Id = Guid.NewGuid(), BusinessDomainName = "Name 1"},
                new BusinessDomainResponseModel { Id = Guid.NewGuid(), BusinessDomainName = "Name 2"}
            }.AsEnumerable();

            _mockBusinessDomainDataService.Setup(x => x.FetchBusinessDomain(It.IsAny<FetchBusinessDomainQuery>()))
                                        .Returns(Task.FromResult(businessDomains));

            var result = await _fecthBusinessDomainQueryHandler.Handle(new FetchBusinessDomainQuery(), new CancellationToken());
            Assert.Equal(businessDomains.Count(), result.Count());
            Assert.Equal(businessDomains.First().BusinessDomainName, "Name 1");
        }

        [Fact]
        public async Task Handler_GetBusinessDomainDetailHandlerSuccess_ReturnBusinessDomainResponseModel()
        {
            var businessDomain = new BusinessDomainResponseModel
            {
                Id = Guid.NewGuid(),
                BusinessDomainName = "Name 1"
            };

            _mockBusinessDomainDataService.Setup(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()))
                                            .Returns(Task.FromResult(businessDomain));

            var result = await _getBusinessDomainDetailHandler.Handle(new GetBusinessDomainDetailsQuery(), new CancellationToken());
            Assert.NotNull(result);
            Assert.Equal(businessDomain.BusinessDomainName, "Name 1");
        }
    }
}