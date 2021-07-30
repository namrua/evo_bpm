using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Command;
using FluentValidation;

namespace CleanEvoBPM.Application.Validator.HolidayPlan
{
    public class CreateProjectHolidayPlanCommandValidator : AbstractValidator<CreateProjectHolidayPlanCommand>
    {
        public CreateProjectHolidayPlanCommandValidator()
        {
            RuleFor(v => v.ProjectId)
                .NotEmpty().WithMessage("Project must not be empty");

            RuleFor(v => v.ResourceId)
               .NotEmpty().WithMessage("Resource must not be empty");

            RuleFor(v => v.ResourceRoleId)
               .NotEmpty().WithMessage("Role must not be empty");

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
