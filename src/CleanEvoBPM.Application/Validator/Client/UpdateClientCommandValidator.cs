using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Client.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Validator.Client
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        private readonly IGenericDataService<UpdateClientCommand> _genericDataService;
        public UpdateClientCommandValidator(IGenericDataService<UpdateClientCommand> genericDataService)
        {
            _genericDataService = genericDataService;

            RuleFor(v => v.ClientName)
                .Must(CheckUniqueName).WithMessage(ValidateMessage.UniqueName)
                .MaximumLength(250)
                .Matches("^[^<>$@!&*]+$")
                .NotEmpty();

            RuleFor(v => v.ClientDivisionName)
                .Matches("^[^<>$@!&*]+$").When(x => !string.IsNullOrEmpty(x.ClientDivisionName))
                .MaximumLength(1000);
        }

        private bool CheckUniqueName(UpdateClientCommand item, string clientName)
        {
            return _genericDataService.IsUniqueName("Client", "ClientName", item.ClientName, item.Id.ToString()).Result;
        }
    }
}
