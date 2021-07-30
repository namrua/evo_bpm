using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ServiceType.CommandHandler;
using CleanEvoBPM.Application.CQRS.ServiceType.QueryHandler;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Project;
using CleanEvoBPM.Domain;
using Moq;

namespace CleanEvoBPM.Test.Application.CQRS.ServiceType
{
    public partial class ServiceTypeTest
    {
        public Mock<IServiceTypeDataService> _ServiceTypeDataServiceMock;
        public Mock<IGenericDataService<ProjectMasterDataToDelete>> _genericDataServiceMock;
        public GetServiceTypeQueryHandler _getServiceTypeQueryHandler;
        public CreateServiceTypeCommandHandler _createServiceTypeCommandHandler;
        public UpdateServiceTypeCommandHandler _updateServiceTypeCommandHandler;
        public DeleteServiceTypeCommandHandler _deleteServiceTypeCommandHandler;
        private readonly Mock<INotificationDispatcher> _mockNotificationDispatcher;

        public ServiceTypeTest()
        {
            _ServiceTypeDataServiceMock = new Mock<IServiceTypeDataService>();
            _genericDataServiceMock = new Mock<IGenericDataService<ProjectMasterDataToDelete>>();
            _getServiceTypeQueryHandler = new GetServiceTypeQueryHandler(_ServiceTypeDataServiceMock.Object);
            _mockNotificationDispatcher = new Mock<INotificationDispatcher>();
            _getServiceTypeQueryHandler = new GetServiceTypeQueryHandler(_ServiceTypeDataServiceMock.Object);
            _createServiceTypeCommandHandler = new CreateServiceTypeCommandHandler(_ServiceTypeDataServiceMock.Object,
                _mockNotificationDispatcher.Object);
            _updateServiceTypeCommandHandler = new UpdateServiceTypeCommandHandler(_ServiceTypeDataServiceMock.Object,
                _mockNotificationDispatcher.Object);
            _deleteServiceTypeCommandHandler = new DeleteServiceTypeCommandHandler(_ServiceTypeDataServiceMock.Object,
            _genericDataServiceMock.Object,
                _mockNotificationDispatcher.Object);
        }
    }
}
