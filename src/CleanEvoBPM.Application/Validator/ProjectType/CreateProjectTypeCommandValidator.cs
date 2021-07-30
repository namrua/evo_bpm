using FluentValidation;
using CleanEvoBPM.Application.CQRS.ProjectType.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Common;

namespace CleanEvoBPM.Application.Validator
{
    public class CreateProjectTypeCommandValidator : AbstractValidator<CreateProjectTypeCommand>
    {
        private readonly IGenericDataService<CreateProjectTypeCommand> _genericDataService;
        public CreateProjectTypeCommandValidator(IGenericDataService<CreateProjectTypeCommand> genericDataService)
        {
            _genericDataService = genericDataService;

            RuleFor(v => v.ProjectTypeName)
                .Must(CheckUniqueName).WithMessage(ValidateMessage.UniqueName)
                .MaximumLength(250)
                .Matches("^[^<>$@!&*]+$")
                .NotEmpty();

            RuleFor(v => v.ProjectTypeDescription)
                .Matches("^[^<>$@!&*]+$").When(x => !string.IsNullOrEmpty(x.ProjectTypeDescription))
                .MaximumLength(1000);
        }

        private bool CheckUniqueName(CreateProjectTypeCommand item, string projectTypeName)
        {
            return _genericDataService.IsUniqueName("ProjectType", "ProjectTypeName", item.ProjectTypeName,null).Result;
        }
    }
}
