using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.CQRS.ServiceType.Command;
using FluentValidation.Results;
using CleanEvoBPM.Application.Common.Exceptions;
using CleanEvoBPM.Application.CQRS.ServiceType.Query;
using System.Linq;
using CleanEvoBPM.Domain;
using CleanEvoBPM.Application.CQRS.ServiceType.Event;

namespace CleanEvoBPM.Application.CQRS.ServiceType.CommandHandler
{
    public class UpdateServiceTypeCommandHandler : BaseServiceTypeHandler, IRequestHandler<UpdateServiceTypeCommand, bool>
    {
        private readonly INotificationDispatcher _dispatcher;
        public UpdateServiceTypeCommandHandler(IServiceTypeDataService serviceTypeDataService,
            INotificationDispatcher dispatcher) : base(serviceTypeDataService)
        {
            _dispatcher = dispatcher;
        }

        public async Task<bool> Handle(UpdateServiceTypeCommand request, CancellationToken cancellationToken)
        {
            var existingServiceType = await _serviceTypeDataService.FetchServiceType(new GetServiceTypeQuery { ServiceTypeName = request.ServiceTypeName });

            if (existingServiceType != null && existingServiceType.Any() && existingServiceType.First().Id != request.Id)
            {
                throw new CustomValidationException(new List<ValidationFailure> {
                    new ValidationFailure("ServiceTypeName", "Service Type Name must be unique")
                });
            }

            var result = await _serviceTypeDataService.UpdateServiceType(request);
            await _dispatcher.Push(new UpdateServiceTypeLog(request));
            return result;
        }
    }


}
