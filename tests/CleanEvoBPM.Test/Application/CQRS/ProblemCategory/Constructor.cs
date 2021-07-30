using CleanEvoBPM.Application.CQRS.ProblemCategory.CommandHandle;
using CleanEvoBPM.Application.CQRS.ProblemCategory.QueryHandle;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectLLBP;
using CleanEvoBPM.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Test.Application.CQRS.ProblemCategory
{
    public partial class ProblemCategoryTest
    {
        public Mock<IProblemCategoryService> _problemCategoryService;
        public CreateProblemCategoryCommandHandler _createProblemCategoryCommandHandler;
        public UpdateProblemCategoryCommandHandler _updateProblemCategoryCommandHandler;
        public DeleteProblemCategoryCommandHandler _deleteProblemCategoryCommandHandler;
        public FetchProblemCategoryQueryHandle _fetchProblemCategoryQueryHandle;
        public GetProblemCategoryDetailHandle _getProblemCategoryDetailHandle;
        public Mock<IGenericDataService<ProjectLLBPModel>> _mockGenericDataService;
        private readonly Mock<INotificationDispatcher> _mockNotificationDispatcher;
        public ProblemCategoryTest()
        {
            _problemCategoryService = new Mock<IProblemCategoryService>();
            _mockNotificationDispatcher = new Mock<INotificationDispatcher>();
            _mockGenericDataService = new Mock<IGenericDataService<ProjectLLBPModel>>();
            _createProblemCategoryCommandHandler = new CreateProblemCategoryCommandHandler(_problemCategoryService.Object,
                _mockNotificationDispatcher.Object);
            _updateProblemCategoryCommandHandler = new UpdateProblemCategoryCommandHandler(_problemCategoryService.Object,
                _mockNotificationDispatcher.Object);
            _deleteProblemCategoryCommandHandler = new DeleteProblemCategoryCommandHandler(_problemCategoryService.Object,
                _mockGenericDataService.Object,
                _mockNotificationDispatcher.Object);
            _fetchProblemCategoryQueryHandle = new FetchProblemCategoryQueryHandle(_problemCategoryService.Object);
            _getProblemCategoryDetailHandle = new GetProblemCategoryDetailHandle(_problemCategoryService.Object);
        }
    }
}
