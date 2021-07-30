using CleanEvoBPM.Application.CQRS.ServiceType.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Validator.ServiceType
{
    public class UpdateServiceTypeCommandValidator : AbstractValidator<UpdateServiceTypeCommand>
    {
        public UpdateServiceTypeCommandValidator()
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
