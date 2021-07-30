using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Client.Command;
using CleanEvoBPM.Application.CQRS.Client.Event;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Project;
using CleanEvoBPM.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Client.CommandHandler
{
    public class DeleteClientCommandHandler : BaseClientHandler, IRequestHandler<DeleteClientCommand, GenericResponse>
    {
        public readonly IGenericDataService<ProjectMasterDataToDelete> _genericDataService;
        private readonly INotificationDispatcher _dispatcher;
        public DeleteClientCommandHandler(IClientDataService clientDataService,
        IGenericDataService<ProjectMasterDataToDelete> genericDataService,
        INotificationDispatcher dispatcher)
         : base(clientDataService)
        {
            _genericDataService = genericDataService;
            _dispatcher = dispatcher;
        }
        public async Task<GenericResponse> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var checkList = await _genericDataService.GetAll(TableName.Project);
            if (checkList.Any(x => x.ClientId == request.Id))
                return new GenericResponse
                {
                    Code = 400,
                    Success = false,
                    Message = ValidateMessage.DeleteMasterDataFailed
                };
            await _dispatcher.Push(new DeleteClientLog(request.Id));
            var deleteResult = await _clientDataService.DeleteClient(request);

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