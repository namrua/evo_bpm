using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.Client.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using Xunit;
using Moq;
using CleanEvoBPM.Application.CQRS.Client.QueryHandler;
using CleanEvoBPM.Application.Models.Client;
using System.Linq;

namespace CleanEvoBPM.Test.Application.CQRS.Client.QueryHandler
{

    public class ClientQueryHandlerTest
    {
        private Mock<IClientDataService> _mockClientDataService;
        private FetchClientQueryHandler _fetchClientQueryHandler;
        private GetClientDetailQueryHandler _getClientDetailsHandler;

        public ClientQueryHandlerTest()
        {
            _mockClientDataService = new Mock<IClientDataService>();
            _fetchClientQueryHandler = new FetchClientQueryHandler(_mockClientDataService.Object);
            _getClientDetailsHandler = new GetClientDetailQueryHandler(_mockClientDataService.Object);
        }

        [Fact]
        public async Task Handle_FetchClientQueryHandlerSuccess_ReturnTrue()
        {
            var clientId = Guid.NewGuid();
            var clientResponseModels = new List<ClientResponseModel>
            {
                new ClientResponseModel{Id=clientId, ClientName="Microservice"},
                new ClientResponseModel{Id=Guid.NewGuid(), ClientName="Docker"}
            }.AsEnumerable();

            _mockClientDataService.Setup(x => x.GetClients(It.IsAny<FetchClientQuery>())).Returns(Task.FromResult(clientResponseModels));
            var result = await _fetchClientQueryHandler.Handle(new FetchClientQuery(), new CancellationToken());
            Assert.Equal(clientResponseModels.Count(), result.Count());
            Assert.Equal(clientResponseModels.FirstOrDefault().Id, clientId);
        }

        [Fact]
        public async Task Handle_GetClientDetailsHandlerSuccess_ReturnTrue()
        {
            var clientId = Guid.NewGuid();
            var clientResponseModel = new ClientResponseModel { Id = clientId, ClientName = "Microservice" };
            _mockClientDataService.Setup(x => x.GetClient(It.IsAny<GetClientDetailQuery>())).Returns(Task.FromResult(clientResponseModel));
            var result = await _getClientDetailsHandler.Handle(new GetClientDetailQuery(), new CancellationToken());
            Assert.NotNull(result);
            Assert.Equal(clientId, result.Id);
        }
    }
}
