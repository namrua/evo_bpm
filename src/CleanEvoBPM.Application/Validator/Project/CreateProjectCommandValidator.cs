using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using CleanEvoBPM.Application.CQRS.Project.Command;
using CleanEvoBPM.Application.Validator.CustomValidator;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;

namespace CleanEvoBPM.Application.Validator
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        IGenericDataService<CreateProjectCommand> _genericDataService;

        public CreateProjectCommandValidator(IGenericDataService<CreateProjectCommand> genericDataService)
        {
            _genericDataService = genericDataService;
            var listProject = _genericDataService.GetAll("Project").Result;
            RuleFor(v => v.ProjectCode)
                .NotEmpty()
                .MaximumLength(20)
                .IsUnique(listProject);
            RuleFor(v => v.ProjectName)
               .NotEmpty()
               .MaximumLength(50)
               .IsUnique(listProject);
            RuleFor(v => v.Client.ClientName)
               .NotEmpty()
               .MaximumLength(50);
            RuleFor(v => v.Client.ClientDivisionName)
               .NotEmpty()
               .MaximumLength(50);
            RuleFor(v => v.ProjectTypeId)
               .NotEmpty();
            RuleFor(v => v.ServiceTypeId)
               .NotEmpty();
            RuleFor(v => v.BusinessDomainId)
               .NotEmpty();
            RuleFor(v => v.MethodologyId)
               .NotEmpty();
            RuleFor(v => v.TechnologyId)
               .NotEmpty();
            RuleFor(v => v.StartDate)
               .NotEmpty();
        }
    }
}
