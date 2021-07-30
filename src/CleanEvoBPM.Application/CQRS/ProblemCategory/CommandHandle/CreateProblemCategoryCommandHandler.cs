using CleanEvoBPM.Application.CQRS.ProblemCategory.Command;
using CleanEvoBPM.Application.CQRS.ProblemCategory.Event;
using CleanEvoBPM.Application.CQRS.Technology.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace CleanEvoBPM.Application.CQRS.ProblemCategory.QueryHandle
{
    public class CreateProblemCategoryCommandHandler:BaseProblemCategoryHandle, IRequestHandler<CreateProblemCategoryCommand, bool>
    {
        private readonly INotificationDispatcher _dispatcher;
        public CreateProblemCategoryCommandHandler(IProblemCategoryService problemCategoryService,
            INotificationDispatcher dispatcher) : base(problemCategoryService)
        {
            _dispatcher = dispatcher;
        }

        public async Task<bool> Handle(CreateProblemCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await base._problemCategoryService.Create(request);
            await _dispatcher.Push(new CreateProblemCategoryLog(request));
            return result;
        }
    }
}
