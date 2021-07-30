using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProblemCategory.Command;
using CleanEvoBPM.Application.CQRS.ProblemCategory.Event;
using CleanEvoBPM.Application.CQRS.Technology.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectLLBP;
using CleanEvoBPM.Domain;
using MediatR;
using Microsoft.VisualBasic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.ProblemCategory.CommandHandle
{
    public class DeleteProblemCategoryCommandHandler : BaseProblemCategoryHandle, IRequestHandler<DeleteProblemCategoryCommand, GenericResponse>
    {
        private readonly INotificationDispatcher _dispatcher;
        public readonly IGenericDataService<ProjectLLBPModel> _genericDataService;
        public DeleteProblemCategoryCommandHandler(IProblemCategoryService problemCategoryService,
            IGenericDataService<ProjectLLBPModel> genericDataService,
            INotificationDispatcher dispatcher) : base(problemCategoryService)
        {
            _genericDataService = genericDataService;
            _dispatcher = dispatcher;
        }

        public async Task<GenericResponse> Handle(DeleteProblemCategoryCommand request, CancellationToken cancellationToken)
        {
            var checkList = await _genericDataService.GetAll(TableName.ProjectLLBP);
            if (checkList.Any(x => x.ProblemCategory == request.Id))
                return new GenericResponse
                {
                    Code = 400,
                    Success = false,
                    Message = ValidateMessage.DeleteMasterDataFailed
                };
            
            var deleteResult = await _problemCategoryService.Delete(request);
            if (!deleteResult)
                return new GenericResponse
                {
                    Code = 404,
                    Success = false,
                    Message = ValidateMessage.NotFound
                };
            await _dispatcher.Push(new Event.DeleteProblemCategoryLog(request));
            return new GenericResponse
            {
                Code = 200,
                Success = true,
                Message = ValidateMessage.DeleteSucess
            };
        }
    }
}
