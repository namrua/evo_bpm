using CleanEvoBPM.Application.CQRS.BusinessDomain.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Validator.CustomValidator;
using FluentValidation;

namespace CleanEvoBPM.Application.Validator.BusinessDomain
{
    public class CreateBusinessDomainCommandValidator : AbstractValidator<CreateBusinessDomainCommand>
    {
        private readonly IGenericDataService<CreateBusinessDomainCommand> _genericDataService;

        public CreateBusinessDomainCommandValidator(IGenericDataService<CreateBusinessDomainCommand> genericDataService)
        {
            _genericDataService = genericDataService;
            RuleFor(d => d.BusinessDomainName)
                .IsUnique(_genericDataService.GetAll("BusinessDomain").Result)
                .MaximumLength(250)
                .NotEmpty();
            RuleFor(d => d.Description)
                .MaximumLength(1000);
        }
    }
}