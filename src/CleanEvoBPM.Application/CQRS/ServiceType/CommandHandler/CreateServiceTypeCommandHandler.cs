using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.CQRS.ServiceType.Command;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ServiceType.Query;
using CleanEvoBPM.Application.Common.Exceptions;
using System.Linq;
using FluentValidation.Results;
using CleanEvoBPM.Domain;
using CleanEvoBPM.Application.CQRS.ServiceType.Event;

namespace CleanEvoBPM.Application.CQRS.ServiceType.CommandHandler
{
    public class CreateServiceTypeCommandHandler : BaseServiceTypeHandler, IRequestHandler<CreateServiceTypeCommand, GenericResponse>
    {
        private readonly INotificationDispatcher _dispatcher;
        public CreateServiceTypeCommandHandler(IServiceTypeDataService serviceTypeDataService,
            INotificationDispatcher dispatcher) : base(serviceTypeDataService)
        {
            _dispatcher = dispatcher;
        }

        public async Task<GenericResponse> Handle(CreateServiceTypeCommand request, CancellationToken cancellationToken)
        {
            var existingServiceType = await _serviceTypeDataService.FetchServiceType(new GetServiceTypeQuery { ServiceTypeName = request.ServiceTypeName });

            if (existingServiceType != null && existingServiceType.Any())
            {
                throw new CustomValidationException(new List<ValidationFailure> {
                    new ValidationFailure("ServiceTypeName", "Service Type Name must be unique")
                });
            }

            var result = await _serviceTypeDataService.CreateServiceType(request);
            await _dispatcher.Push(new CreateServiceTypeLog(request));
            return result;
        }
    }


}
