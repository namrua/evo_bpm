using CleanEvoBPM.Application.CQRS.Project.Command;
using CleanEvoBPM.Application.Validator.Project;
using CleanEvoBPM.Test.Application.Constants;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CleanEvoBPM.Test.Application.Validator.Project
{
    public class UpdateProjectCommandValidatorTest
    {
        private UpdateProjectCommandValidator _updateProjectCommandValidator;
        private UpdateProjectCommand Project = new UpdateProjectCommand
        {
            Id = Guid.NewGuid(),
            ProjectName = "Test",
            ProjectCode = "Test",
            ProjectManagerId = Guid.NewGuid(),
            ProjectTypeId = Guid.NewGuid(),
            ServiceTypeId = Guid.NewGuid(),
            BusinessDomainId = new List<Guid> { Guid.NewGuid() },
            MethodologyId = new List<Guid> { Guid.NewGuid() },
            TechnologyId = new List<Guid> { Guid.NewGuid() },
            StatusId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow,
            DeliveryODCId = Guid.NewGuid(),
            DeliveryLocationId = Guid.NewGuid()
        };
        public UpdateProjectCommandValidatorTest()
        {
            _updateProjectCommandValidator = new UpdateProjectCommandValidator();
        }

        [Fact]
        public void ProjectCode_NotEmpty()
        {
            //Setup
            var project = Project;
            project.ProjectCode = "";

            //Run Service
            var result = _updateProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.ProjectCode);
            var error = result.Errors.FirstOrDefault();

            //Assert
            Assert.Equal(FluentValidationErrorCode.NotEmptyValidator, error.ErrorCode);
        }
        [Fact]
        public void ProjectCode_MaximumLength20()
        {
            //Setup
            var project = Project;
            project.ProjectCode = "Tesssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssst";

            //Run Service
            var result = _updateProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.ProjectCode);
            var error = result.Errors.FirstOrDefault();

            //Assert
            Assert.Equal(FluentValidationErrorCode.MaximumLengthValidator, error.ErrorCode);
        }
        [Fact]
        public void ProjectName_NotEmpty()
        {
            //Setup
            var project = Project;
            project.ProjectName = "";

            //Run Service
            var result = _updateProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.ProjectName);
            var error = result.Errors.FirstOrDefault();

            //Assert
            Assert.Equal(FluentValidationErrorCode.NotEmptyValidator, error.ErrorCode);
        }
        [Fact]
        public void ProjectName_MaximumLength50()
        {
            //Setup
            var project = Project;
            project.ProjectName = "Tesssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssst";

            //Run Service
            var result = _updateProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.ProjectName);
            var error = result.Errors.FirstOrDefault();

            //Assert
            Assert.Equal(FluentValidationErrorCode.MaximumLengthValidator, error.ErrorCode);
        }
        [Fact]
        public void ProjectTypeId_NotEmpty()
        {
            //Setup
            var project = Project;
            project.ProjectTypeId = Guid.Empty;

            //Run Service
            var result = _updateProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.ProjectTypeId);
            var error = result.Errors.FirstOrDefault();

            //Assert
            Assert.Equal(FluentValidationErrorCode.NotEmptyValidator, error.ErrorCode);
        }
        [Fact]
        public void ServiceTypeId_NotEmpty()
        {
            //Setup
            var project = Project;
            project.ServiceTypeId = Guid.Empty;

            //Run Service
            var result = _updateProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.ServiceTypeId);
            var error = result.Errors.FirstOrDefault();

            //Assert
            Assert.Equal(FluentValidationErrorCode.NotEmptyValidator, error.ErrorCode);
        }
        [Fact]
        public void BusinessDomainId_NotEmpty()
        {
            //Setup
            var project = Project;
            project.BusinessDomainId = new List<Guid> { };

            //Run Service
            var result = _updateProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.BusinessDomainId);
            var error = result.Errors.FirstOrDefault();

            //Assert
            Assert.Equal(FluentValidationErrorCode.NotEmptyValidator, error.ErrorCode);
        }
        [Fact]
        public void TechnologyId_NotEmpty()
        {
            //Setup
            var project = Project;
            project.TechnologyId = new List<Guid> { };

            //Run Service
            var result = _updateProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.TechnologyId);
            var error = result.Errors.FirstOrDefault();

            //Assert
            Assert.Equal(FluentValidationErrorCode.NotEmptyValidator, error.ErrorCode);
        }
        [Fact]
        public void StartDate_NotEmpty()
        {
            //Setup
            var project = Project;
            project.StartDate = DateTime.MinValue;

            //Run Service
            var result = _updateProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.StartDate);
            var error = result.Errors.FirstOrDefault();

            //Assert
            Assert.Equal(FluentValidationErrorCode.NotEmptyValidator, error.ErrorCode);
        }
    }
}
