using CleanEvoBPM.Application.CQRS.Status.Command;
using CleanEvoBPM.Application.CQRS.Status.Event;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Status.CommandHandler
{
    public class UpdateStatusCommandHandler : BaseStatusHandler, IRequestHandler<UpdateStatusCommand, bool>
    {
        private readonly INotificationDispatcher _dispatcher;
        public UpdateStatusCommandHandler(IStatusDataService statusDataService,
            INotificationDispatcher dispatcher) : base(statusDataService)
        {
            _dispatcher = dispatcher;
        }

        public async Task<bool> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
        {
            var result = await base._statusDataService.UpdateStatus(request);
            await _dispatcher.Push(new UpdateStatusLog(request));
            return result;
        }
    }
}
