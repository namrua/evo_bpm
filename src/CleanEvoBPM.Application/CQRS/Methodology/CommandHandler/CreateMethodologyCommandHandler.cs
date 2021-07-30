using System;
using System.Collections.Generic;
using System.Text;
using CleanEvoBPM.Application.CQRS.Methodology.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Common.Exceptions;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Methodology.Query;
using FluentValidation.Results;
using System.Linq;
using CleanEvoBPM.Domain;
using CleanEvoBPM.Application.CQRS.Methodology.Event;

namespace CleanEvoBPM.Application.CQRS.Methodology.CommandHandler
{
    public class CreateMethodologyCommandHandler : BaseMethodologyHandler, IRequestHandler<CreateMethodologyCommand, GenericResponse>
    {
        private readonly INotificationDispatcher _dispatcher;
        public CreateMethodologyCommandHandler(IMethodologyDataService methodologyDataService,
            INotificationDispatcher dispatcher) : base (methodologyDataService)
        {
            _dispatcher = dispatcher;
        }

        public async Task<GenericResponse> Handle(CreateMethodologyCommand request, CancellationToken cancellationToken)
        {
            var existingMethodology = await _methodologyDataService.FetchMethodology(new GetMethodologyQuery { MethodologyName = request.MethodologyName });

            if (existingMethodology != null && existingMethodology.Any())
            {
                throw new CustomValidationException(new List<ValidationFailure> {
                    new ValidationFailure("MethodologyName", "Methodology Name must be unique")
                });
            }

            var result = await base._methodologyDataService.CreateMethodology(request);
            await _dispatcher.Push(new CreateMethodologyLog(request));
            return result;
        }
    }
}
