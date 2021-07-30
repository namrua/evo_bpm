using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Command;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Event;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Domain;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.BusinessDomain.CommandHandler
{
    public class UpdateBusinessDomainCommandHandler : BaseBusinessDomainHandler,
        IRequestHandler<UpdateBusinessDomainCommand, bool>
    {
        private readonly INotificationDispatcher _dispatcher;
        public UpdateBusinessDomainCommandHandler(IBusinessDomainDataService businessDomainDataService,
            INotificationDispatcher dispatcher) : base(businessDomainDataService)
        {
            _dispatcher = dispatcher;
        }

        public async Task<bool> Handle(UpdateBusinessDomainCommand request, CancellationToken cancellationToken)
        {
            var result = await _businessDomainDataService.UpdateBusinessDomain(request);
            await _dispatcher.Push(new UpdateBusinessDomainLog(request));
            return result;
        }
    }
}