using CleanEvoBPM.Application.Common.Exceptions;
using CleanEvoBPM.Application.CQRS.Methodology.Command;
using CleanEvoBPM.Application.CQRS.Methodology.Event;
using CleanEvoBPM.Application.CQRS.Methodology.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Domain;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Methodology.CommandHandler
{

    public class UpdateMethodologyCommandHandler : BaseMethodologyHandler, IRequestHandler<UpdateMethodologyCommand, bool>
    {
        private readonly INotificationDispatcher _dispatcher;
        public UpdateMethodologyCommandHandler(IMethodologyDataService methodologyDataService,
            INotificationDispatcher dispatcher) : base(methodologyDataService)
        {
            _dispatcher = dispatcher;
        }

        public async Task<bool> Handle(UpdateMethodologyCommand request, CancellationToken cancellationToken)
        {
            var existingMethodology = await _methodologyDataService.FetchMethodology( new GetMethodologyQuery { MethodologyName = request.MethodologyName });

            if (existingMethodology != null && existingMethodology.Any() && existingMethodology.First().Id != request.Id)
            {
                throw new CustomValidationException(new List<ValidationFailure> {
                    new ValidationFailure("MethodologyName", "Methodology Name must be unique")
                });
            }

            var result = await _methodologyDataService.UpdateMethodology(request);
            await _dispatcher.Push(new UpdateMethodologyLog(request));
            return result;
        }
    }
}

