using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Technology.Command;
using CleanEvoBPM.Application.CQRS.Technology.Event;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Technology.CommandHandler
{
    public class UpdateTechnologyCommandHandler : BaseTechnologyHandler, IRequestHandler<UpdateTechnologyCommand, GenericResponse>
    {
        private readonly INotificationDispatcher _dispatcher;
        public UpdateTechnologyCommandHandler(ITechnologyDataService technologyDataService,
            INotificationDispatcher dispatcher) : base(technologyDataService)
        {
            _dispatcher = dispatcher;
        }

        public async Task<GenericResponse> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
        {
            var result = await base._technologyDataService.Update(request);
            var parameters = new TechnologyCommit
            {
                Id = request.Id,
                TechnologyName = request.TechnologyName,
                TechnologyDescription = request.TechnologyDescription,
                TechnologyActive = request.TechnologyActive,
                CreatedDate = request.CreatedDate
            };
            await _dispatcher.Push(new UpdateTechnologyLog(parameters));
            return result;
        }
    }
}
