using CleanEvoBPM.Application.CQRS.Project.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Validator.CustomValidator;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.Validator.Project
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(v => v.ProjectCode)
                .MaximumLength(20)
                .NotEmpty();
            RuleFor(v => v.ProjectName)
               .MaximumLength(50)
               .NotEmpty();
            RuleFor(v => v.ProjectTypeId)
               .NotEmpty();
            RuleFor(v => v.ServiceTypeId)
               .NotEmpty();
            RuleFor(v => v.BusinessDomainId)
               .NotEmpty();
            RuleFor(v => v.TechnologyId)
               .NotEmpty();
            RuleFor(v => v.StartDate)
               .NotEmpty();
        }
    }
}
