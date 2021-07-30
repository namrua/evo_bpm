using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProjectType.Command;
using CleanEvoBPM.Application.CQRS.Technology.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Validator.Technology
{
    public class UpdateTechnologyCommandValidator : AbstractValidator<UpdateTechnologyCommand>
    {
        private readonly IGenericDataService<UpdateTechnologyCommand> _genericDataService;
        public UpdateTechnologyCommandValidator(IGenericDataService<UpdateTechnologyCommand> genericDataService)
        {
            _genericDataService = genericDataService;

            RuleFor(v => v.TechnologyName)
                .Must(CheckUniqueName).WithMessage(ValidateMessage.UniqueName)
                .MaximumLength(250)
                .Matches("^[^<>$@!&*]+$")
                .NotEmpty();

            RuleFor(v => v.TechnologyDescription)
                .Matches("^[^<>$@!&*]+$").When(x => !string.IsNullOrEmpty(x.TechnologyDescription))
                .MaximumLength(1000);
        }

        private bool CheckUniqueName(UpdateTechnologyCommand item, string technologyName)
        {
            return _genericDataService.IsUniqueName("Technology", "TechnologyName", item.TechnologyName, item.Id.ToString()).Result;
        }
    }
}
