using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Client.Command;
using CleanEvoBPM.Application.CQRS.Client.Event;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Client.CommandHandler
{
    public class CreateClientCommandHandler : BaseClientHandler,
        IRequestHandler<CreateClientCommand, GenericResponse>
    {
        private readonly INotificationDispatcher _dispatcher;
        public CreateClientCommandHandler(IClientDataService clientDataService,
            INotificationDispatcher dispatcher) : base(clientDataService)
        {
            _dispatcher = dispatcher;
        }

        public async Task<GenericResponse> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var result = await base._clientDataService.CreateClient(request);
            await _dispatcher.Push(new CreateClientLog(request));
            return result;
        }

    }
}