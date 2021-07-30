using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Technology.Command;
using CleanEvoBPM.Application.CQRS.Technology.Event;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectTechnology;
using CleanEvoBPM.Domain;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.Technology.CommandHandler
{
    public class DeleteTechnologyCommandHandler : BaseTechnologyHandler, IRequestHandler<DeleteTechnologyCommand, GenericResponse>
    {
        private readonly INotificationDispatcher _dispatcher;
        public readonly IGenericDataService<ProjectTechnologyModel> _genericDataService;
        public DeleteTechnologyCommandHandler(ITechnologyDataService technologyDataService,
        IGenericDataService<ProjectTechnologyModel> genericDataService,
        INotificationDispatcher dispatcher
        ) : base(technologyDataService)
        {
            _genericDataService = genericDataService;
            _dispatcher = dispatcher;
        }
        public async Task<GenericResponse> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
        {
            var checkList = await _genericDataService.GetAll(TableName.ProjectTechnology);
            if (checkList.Any(x => x.TechnologyId == request.Id))
                return new GenericResponse
                {
                    Code = 400,
                    Success = false,
                    Message = ValidateMessage.DeleteMasterDataFailed
                };
            var parameters = new TechnologyCommit
            {
                Id = request.Id
            };
            await _dispatcher.Push(new DeleteTechnologyLog(parameters));
            var deleteResult = await _technologyDataService.Delete(request.Id);
            if (!deleteResult)
                return new GenericResponse
                {
                    Code = 404,
                    Success = false,
                    Message = ValidateMessage.NotFound
                };
            return new GenericResponse
            {
                Code = 200,
                Success = true,
                Message = ValidateMessage.DeleteSucess
            };
        }
    }
}
