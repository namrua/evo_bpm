using CleanEvoBPM.Application.CQRS.Methodology.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Validator.CustomValidator;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Validator.Methodology
{
    public class CreateMethodologyCommandValidator : AbstractValidator<CreateMethodologyCommand>
    {
        public CreateMethodologyCommandValidator()
        {
            RuleFor(v => v.MethodologyName)
                .MinimumLength(5)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(v => v.Description)
               .MaximumLength(1000);
        }
    }    
}
