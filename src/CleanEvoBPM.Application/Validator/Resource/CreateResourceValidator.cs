using CleanEvoBPM.Application.CQRS.Resource.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Validator.Resource
{
    public class CreateResourceValidator : AbstractValidator<CreateResourceCommand>
    {
        public CreateResourceValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Resource can not be empty");

            RuleFor(v => v.RoleId)
               .NotEmpty().WithMessage("Resource role can not be empty");

            RuleFor(v => v.Email)
               .NotEmpty().WithMessage("Email can not be empty");

            RuleFor(v => v.FromDate)
                .Must(d => !d.Equals(default)).WithMessage("'From Date' must not be empty");

            RuleFor(v => v.ToDate)
                .Must(d => !d.Equals(default)).WithMessage("'To Date' must not be empty")
                .GreaterThanOrEqualTo(d => d.FromDate).WithMessage("'To Date' must not be smaller than From Date");

            RuleFor(v => v.Note)
               .MaximumLength(1000);
        }
    }
}
