using CleanEvoBPM.Application.CQRS.ServiceType.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Validator.CustomValidator;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Validator.ServiceType
{
    public class CreateServiceTypeCommandValidator : AbstractValidator<CreateServiceTypeCommand>
    {
        public CreateServiceTypeCommandValidator()
        {
            RuleFor(v => v.ServiceTypeName)
                .MinimumLength(5)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(v => v.Description)
                .MaximumLength(1000);
        }
    }
}
