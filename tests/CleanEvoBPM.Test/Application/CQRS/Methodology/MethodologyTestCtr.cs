using CleanEvoBPM.Application.CQRS.Methodology.CommandHandler;
using CleanEvoBPM.Application.CQRS.Methodology.QueryHandler;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectMethodology;
using CleanEvoBPM.Domain;
using Moq;

namespace CleanEvoBPM.Test.Application.CQRS.Methodology
{
    public partial class MethodologyTest
    {
        public Mock<IMethodologyDataService> _methodologyDataServiceMock;
        public GetMethodologyQueryHandler _getMethodologyQueryHandler;
        public CreateMethodologyCommandHandler _createMethodologyCommandHandler;
        public UpdateMethodologyCommandHandler _updateMethodologyCommandHandler;
        public DeleteMethodologyCommandHandler _deleteMethodologyCommandHandler;
        public Mock<IGenericDataService<ProjectMethodologyModel>> _mockGenericDataService;
         private readonly Mock<INotificationDispatcher> _mockNotificationDispatcher;

        public MethodologyTest()
        {
            _methodologyDataServiceMock = new Mock<IMethodologyDataService>();
            _mockNotificationDispatcher = new Mock<INotificationDispatcher>();
            _mockGenericDataService = new Mock<IGenericDataService<ProjectMethodologyModel>>();
            _getMethodologyQueryHandler = new GetMethodologyQueryHandler(_methodologyDataServiceMock.Object);
            _createMethodologyCommandHandler = new CreateMethodologyCommandHandler(_methodologyDataServiceMock.Object,
                _mockNotificationDispatcher.Object);
            _updateMethodologyCommandHandler = new UpdateMethodologyCommandHandler(_methodologyDataServiceMock.Object,
                _mockNotificationDispatcher.Object);
            _deleteMethodologyCommandHandler = new DeleteMethodologyCommandHandler(_methodologyDataServiceMock.Object,
            _mockGenericDataService.Object,
                _mockNotificationDispatcher.Object);
        }
    }
}
