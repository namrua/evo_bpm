using CleanEvoBPM.Application.Common;
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
    public class CreateStatusCommandHandler : BaseStatusHandler, IRequestHandler<CreateStatusCommand, GenericResponse>
    {
        private readonly INotificationDispatcher _dispatcher;
        public CreateStatusCommandHandler(IStatusDataService statusDataService,
            INotificationDispatcher dispatcher) : base(statusDataService)
        {
            _dispatcher = dispatcher;
        }

        public async Task<GenericResponse> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
        {
            var result = await base._statusDataService.CreateStatus(request);
            await _dispatcher.Push(new CreateStatusLog(request));
            return result;
        }
    }
}
