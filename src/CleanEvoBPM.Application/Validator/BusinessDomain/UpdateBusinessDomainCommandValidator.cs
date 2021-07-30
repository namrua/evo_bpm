using CleanEvoBPM.Application.CQRS.BusinessDomain.Command;
using FluentValidation;

namespace CleanEvoBPM.Application.Validator.BusinessDomain
{
    public class UpdateBusinessDomainCommandValidator : AbstractValidator<UpdateBusinessDomainCommand>
    {
        public UpdateBusinessDomainCommandValidator()
        {
            RuleFor(d => d.BusinessDomainName)
            .MaximumLength(250)
            .NotEmpty();
            RuleFor(d => d.Description)
            .MaximumLength(1000);
        }
    }
}