using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Technology.Command;
using CleanEvoBPM.Application.CQRS.Technology.Event;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Technology.CommandHandler
{
    public class CreateTechnologyCommandHandler : BaseTechnologyHandler, IRequestHandler<CreateTechnologyCommand, GenericResponse> 
    {
        private readonly INotificationDispatcher _dispatcher;
        public CreateTechnologyCommandHandler(ITechnologyDataService technologyDataService,
            INotificationDispatcher dispatcher) : base(technologyDataService)
        {
            _dispatcher = dispatcher;
        }

        public async Task<GenericResponse> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
        {
            var result = await base._technologyDataService.Create(request);
            var data = new TechnologyCommit
            {
                TechnologyName = request.TechnologyName,
                TechnologyDescription = request.TechnologyDescription,
                TechnologyActive = request.TechnologyActive,
                IsDeleted = false
            };
            await _dispatcher.Push(new CreateTechnologyLog(data));
            return result;
        }
    }
}
