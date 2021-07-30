using CleanEvoBPM.Application.CQRS.ProblemCategory.Command;
using CleanEvoBPM.Application.CQRS.ProblemCategory.Event;
using CleanEvoBPM.Application.CQRS.Technology.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProblemCategory;
using CleanEvoBPM.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace CleanEvoBPM.Application.CQRS.ProblemCategory.CommandHandle
{
    public class UpdateProblemCategoryCommandHandler : BaseProblemCategoryHandle, IRequestHandler<UpdateProblemCategoryCommand, bool>
    {
        private readonly INotificationDispatcher _dispatcher;
        public UpdateProblemCategoryCommandHandler(IProblemCategoryService problemCategoryService,
            INotificationDispatcher dispatcher) :base(problemCategoryService)
        {
            _dispatcher = dispatcher;
        }

        public async Task<bool> Handle(UpdateProblemCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await base._problemCategoryService.Update(request);
            await _dispatcher.Push(new UpdateProblemCategoryLog(request));
            return result;
        }
    }
}
