using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProblemCategory.Command;
using CleanEvoBPM.Application.CQRS.Technology.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using FluentValidation;
namespace CleanEvoBPM.Application.Validator.ProblemCategory
{
    public class UpdateProblemCategoryCommandValidator : AbstractValidator<UpdateProblemCategoryCommand>
    {
        private readonly IGenericDataService<UpdateProblemCategoryCommand> _genericDataService;
        public UpdateProblemCategoryCommandValidator(IGenericDataService<UpdateProblemCategoryCommand> genericDataService)
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

        private bool CheckUniqueName(UpdateProblemCategoryCommand item, string technologyName)
        {
            return _genericDataService.IsUniqueName("ProblemCategory", "Name", item.Name, item.Id.ToString()).Result;
        }
    }
}
