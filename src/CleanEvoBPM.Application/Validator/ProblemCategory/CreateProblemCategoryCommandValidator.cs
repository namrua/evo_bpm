using System;
using System.Collections.Generic;
using System.Text;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProblemCategory.Command;
using CleanEvoBPM.Application.CQRS.Technology.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using FluentValidation;

namespace CleanEvoBPM.Application.Validator.ProblemCategory
{
    public class CreateProblemCategoryCommandValidator : AbstractValidator<CreateProblemCategoryCommand>
    {
        private readonly IGenericDataService<CreateProblemCategoryCommand> _genericDataService;
        public CreateProblemCategoryCommandValidator(IGenericDataService<CreateProblemCategoryCommand> genericDataService)
        {
            _genericDataService = genericDataService;

            RuleFor(v => v.Name)
                .Must(CheckUniqueName).WithMessage(ValidateMessage.UniqueName)
                .MaximumLength(250)
                .Matches("^[^<>$@!&*]+$")
                .NotEmpty();

            RuleFor(v => v.Description)
                .Matches("^[^<>$@!&*]+$").When(x => !string.IsNullOrEmpty(x.Description))
                .MaximumLength(1000);
        }

        private bool CheckUniqueName(CreateProblemCategoryCommand item, string name)
        {
            return _genericDataService.IsUniqueName("ProblemCategory", "Name", item.Name, null).Result;
        }
    }
}
