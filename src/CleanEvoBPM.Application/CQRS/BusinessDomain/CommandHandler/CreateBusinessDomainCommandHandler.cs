using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Command;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Event;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Domain;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.BusinessDomain.CommandHandler
{
    public class CreateBusinessDomainCommandHandler : BaseBusinessDomainHandler,IRequestHandler<CreateBusinessDomainCommand, GenericResponse>
    {
        private readonly INotificationDispatcher _dispatcher;
        public CreateBusinessDomainCommandHandler(IBusinessDomainDataService businessDomainDataService, 
                INotificationDispatcher dispatcher) : base(businessDomainDataService)
        {
            _dispatcher = dispatcher;
        }

        public async Task<GenericResponse> Handle(CreateBusinessDomainCommand request, CancellationToken cancellationToken)
        {
            var result = await base._businessDomainDataService.CreatBusinessDomain(request);
            await _dispatcher.Push(new CreateBusinessDomainLog(request));
            return result;
        }
    }
}