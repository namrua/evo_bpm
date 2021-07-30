using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Query;
using CleanEvoBPM.Application.CQRS.Client.Query;
using CleanEvoBPM.Application.CQRS.Methodology.Query;
using CleanEvoBPM.Application.CQRS.Project.Query;
using CleanEvoBPM.Application.CQRS.Project.QueryHandler;
using CleanEvoBPM.Application.CQRS.ProjectType.Query;
using CleanEvoBPM.Application.CQRS.ServiceType.Query;
using CleanEvoBPM.Application.CQRS.Status.Query;
using CleanEvoBPM.Application.CQRS.Technology.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.BusinessDomain;
using CleanEvoBPM.Application.Models.Client;
using CleanEvoBPM.Application.Models.Methodology;
using CleanEvoBPM.Application.Models.Project;
using CleanEvoBPM.Application.Models.ProjectType;
using CleanEvoBPM.Application.Models.ServiceType;
using CleanEvoBPM.Application.Models.Status;
using CleanEvoBPM.Application.Models.Technology;
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
    public class GetProjectByIdHandlerTest
    {
        private readonly Mock<IProjectDataService> _mockProjectDataService;
        private readonly Mock<IBusinessDomainDataService> _mockBusinessDomainDataService;
        private readonly Mock<IClientDataService> _mockClientDataService;
        private readonly Mock<IProjectTypeDataService> _mockProjectTypeDataService;
        private readonly Mock<IServiceTypeDataService> _mockServiceTypeDataService;
        private readonly Mock<ITechnologyDataService> _mockTechnologyDataService;
        private readonly Mock<IMethodologyDataService> _mockMethodologyDataService;
        private readonly Mock<IStatusDataService> _mockStatusDataService;
        private readonly GetProjectByIdHandler _getProjectByIdHandler;
        private readonly Mock<IDeliveryODCDataService> _mockDeliveryODCDataService;
        private readonly Mock<IDeliveryLocationDataService> _mockdDeliveryLocationDataService;
        public GetProjectByIdHandlerTest()
        {
            _mockProjectDataService = new Mock<IProjectDataService>();
            _mockBusinessDomainDataService = new Mock<IBusinessDomainDataService>();
            _mockClientDataService = new Mock<IClientDataService>();
            _mockProjectTypeDataService = new Mock<IProjectTypeDataService>();
            _mockServiceTypeDataService = new Mock<IServiceTypeDataService>();
            _mockTechnologyDataService = new Mock<ITechnologyDataService>();
            _mockMethodologyDataService = new Mock<IMethodologyDataService>();
            _mockStatusDataService = new Mock<IStatusDataService>();
            _mockDeliveryODCDataService = new Mock<IDeliveryODCDataService>();
            _mockdDeliveryLocationDataService = new Mock<IDeliveryLocationDataService>();
            _getProjectByIdHandler = new GetProjectByIdHandler(
                _mockProjectDataService.Object,
                _mockBusinessDomainDataService.Object,
                _mockClientDataService.Object,
                _mockProjectTypeDataService.Object,
                _mockServiceTypeDataService.Object,
                _mockTechnologyDataService.Object,
                _mockMethodologyDataService.Object,
                _mockStatusDataService.Object,
                _mockDeliveryODCDataService.Object,
                _mockdDeliveryLocationDataService.Object);
        }

        [Fact]
        public async Task Handle_Success_ReturnProjectResponseModel()
        {
            //Setup
            var request = new GetProjectById() { Id = Guid.NewGuid()};

            //Mock
            _mockProjectDataService.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ProjectResponseModel { ProjectName ="Test" }));

            _mockBusinessDomainDataService.Setup(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()))
                .Returns(Task.FromResult(new BusinessDomainResponseModel()));

            _mockClientDataService.Setup(x => x.GetClient(It.IsAny<GetClientDetailQuery>()))
                .Returns(Task.FromResult(new ClientResponseModel()));

            _mockProjectTypeDataService.Setup(x => x.FetchProjectType(It.IsAny<GetProjectTypeQuery>()))
                .Returns(Task.FromResult(new List<ProjectTypeResponseModel>().AsEnumerable()));

            _mockServiceTypeDataService.Setup(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()))
                .Returns(Task.FromResult(new List<ServiceTypeResponseModel>().AsEnumerable()));

            _mockMethodologyDataService.Setup(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()))
                .Returns(Task.FromResult(new List<MethodologyResponseModel>().AsEnumerable()));

            _mockTechnologyDataService.Setup(x => x.GetTechnologyDetail(It.IsAny<GetTechnologyDetailsQuery>()))
                .Returns(Task.FromResult(new TechnologyResponseModel()));

            _mockStatusDataService.Setup(x => x.GetStatusDetails(It.IsAny<GetStatusDetailsQuery>()))
                .Returns(Task.FromResult(new GenericResponse<StatusResponseModel>()));

            //Run Service
            var result = await _getProjectByIdHandler.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            Assert.IsType<GenericResponse<ProjectResponseByIdModel>>(result);
            Assert.Equal("Test", result.Content.ProjectName);

            //Verify
            _mockProjectDataService.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mockBusinessDomainDataService.Verify(x => x.GetBusinessDomainDetail(It.IsAny<GetBusinessDomainDetailsQuery>()), Times.Never);
            _mockClientDataService.Verify(x => x.GetClient(It.IsAny<GetClientDetailQuery>()), Times.Once);
            _mockProjectTypeDataService.Verify(x => x.FetchProjectType(It.IsAny<GetProjectTypeQuery>()), Times.Once);
            _mockServiceTypeDataService.Verify(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()), Times.Once);
            _mockMethodologyDataService.Verify(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()), Times.Once);
            _mockTechnologyDataService.Verify(x => x.GetTechnologyDetail(It.IsAny<GetTechnologyDetailsQuery>()), Times.Never);
            _mockStatusDataService.Verify(x => x.GetStatusDetails(It.IsAny<GetStatusDetailsQuery>()), Times.Once);
        }
    }
}
