using CleanEvoBPM.Application.CQRS.Project.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Validator;
using CleanEvoBPM.Test.Application.Constants;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.Application.Validator.Project
{
    public class CreateProjectCommandValidatorTest
    {
        private CreateProjectCommandValidator _createProjectCommandValidator;
        private Mock<IGenericDataService<CreateProjectCommand>> _mockGenericDataService;
        private CreateProjectCommand Project = new CreateProjectCommand
        {
            ProjectName = "Test",
            ProjectCode = "Test",
            Client = new CleanEvoBPM.Application.CQRS.Project.Command.Client
            {
                ClientName = "Test",
                ClientDivisionName = "Test"
            },
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
        public CreateProjectCommandValidatorTest()
        {
            _mockGenericDataService = new Mock<IGenericDataService<CreateProjectCommand>>();
            _createProjectCommandValidator = new CreateProjectCommandValidator(_mockGenericDataService.Object);
        }

        [Fact]
        public void ProjectName_NotEmpty()
        {
            //Setup
            var project = Project;
            project.ProjectName = "";
            var listProjectReturn = new List<CreateProjectCommand>
            {
                new CreateProjectCommand()
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            //Run Service
            var result = _createProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.ProjectName);
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
            var listProjectReturn = new List<CreateProjectCommand>
            {
                new CreateProjectCommand()
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            //Run Service
            var result = _createProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.ProjectCode);
            var error = result.Errors.FirstOrDefault();

            //Assert
            Assert.Equal(FluentValidationErrorCode.MaximumLengthValidator, error.ErrorCode);
        }

        [Fact]
        public void ProjectCode_NotEmpty()
        {
            //Setup
            var project = Project;
            project.ProjectCode = "";
            var listProjectReturn = new List<CreateProjectCommand>
            {
                new CreateProjectCommand()
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            //Run Service
            var result = _createProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.ProjectCode);
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
            var listProjectReturn = new List<CreateProjectCommand>
            {
                new CreateProjectCommand()
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            //Run Service
            var result = _createProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.ProjectName);
            var error = result.Errors.FirstOrDefault();

            //Assert
            Assert.Equal(FluentValidationErrorCode.MaximumLengthValidator, error.ErrorCode);
        }

        [Fact]
        public void ClientName_MaximumLength20()
        {
            //Setup
            var project = Project;
            project.Client.ClientName = "Tesssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssst";
            var listProjectReturn = new List<CreateProjectCommand>
            {
                new CreateProjectCommand()
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            //Run Service
            var result = _createProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.Client.ClientName);
            var error = result.Errors.FirstOrDefault();

            //Assert
            Assert.Equal(FluentValidationErrorCode.MaximumLengthValidator, error.ErrorCode);
        }

        [Fact]
        public void ClientName_NotEmpty()
        {
            //Setup
            var project = Project;
            project.Client.ClientName = "";
            var listProjectReturn = new List<CreateProjectCommand>
            {
                new CreateProjectCommand()
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            //Run Service
            var result = _createProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.Client.ClientName);
            var error = result.Errors.FirstOrDefault();

            //Assert
            Assert.Equal(FluentValidationErrorCode.NotEmptyValidator, error.ErrorCode);
        }

        [Fact]
        public void ClientDivisionName_MaximumLength20()
        {
            //Setup
            var project = Project;
            project.Client.ClientDivisionName = "Tesssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssst";
            var listProjectReturn = new List<CreateProjectCommand>
            {
                new CreateProjectCommand()
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            //Run Service
            var result = _createProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.Client.ClientDivisionName);
            var error = result.Errors.FirstOrDefault();

            //Assert
            Assert.Equal(FluentValidationErrorCode.MaximumLengthValidator, error.ErrorCode);
        }

        [Fact]
        public void ClientDivisionName_NotEmpty()
        {
            //Setup
            var project = Project;
            project.Client.ClientDivisionName = "";
            var listProjectReturn = new List<CreateProjectCommand>
            {
                new CreateProjectCommand()
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            //Run Service
            var result = _createProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.Client.ClientDivisionName);
            var error = result.Errors.FirstOrDefault();

            //Assert
            Assert.Equal(FluentValidationErrorCode.NotEmptyValidator, error.ErrorCode);
        }

        [Fact]
        public void ProjectTypeId_NotEmpty()
        {
            //Setup
            var project = Project;
            project.ProjectTypeId = Guid.Empty;
            var listProjectReturn = new List<CreateProjectCommand>
            {
                new CreateProjectCommand()
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            //Run Service
            var result = _createProjectCommandValidator.TestValidate(project);
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
            var listProjectReturn = new List<CreateProjectCommand>
            {
                new CreateProjectCommand()
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            //Run Service
            var result = _createProjectCommandValidator.TestValidate(project);
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
            var listProjectReturn = new List<CreateProjectCommand>
            {
                new CreateProjectCommand()
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            //Run Service
            var result = _createProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.BusinessDomainId);
            var error = result.Errors.FirstOrDefault();

            //Assert
            Assert.Equal(FluentValidationErrorCode.NotEmptyValidator, error.ErrorCode);
        }

        [Fact]
        public void MethodologyId_NotEmpty()
        {
            //Setup
            var project = Project;
            project.MethodologyId = new List<Guid> { };
            var listProjectReturn = new List<CreateProjectCommand>
            {
                new CreateProjectCommand()
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            //Run Service
            var result = _createProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.MethodologyId);
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
            var listProjectReturn = new List<CreateProjectCommand>
            {
                new CreateProjectCommand()
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            //Run Service
            var result = _createProjectCommandValidator.TestValidate(project);
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
            var listProjectReturn = new List<CreateProjectCommand>
            {
                new CreateProjectCommand()
            };

            //Mock
            _mockGenericDataService.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns(Task.FromResult(listProjectReturn.AsEnumerable()));

            //Run Service
            var result = _createProjectCommandValidator.TestValidate(project);
            result.ShouldHaveValidationErrorFor(t => t.StartDate);
            var error = result.Errors.FirstOrDefault();

            //Assert
            Assert.Equal(FluentValidationErrorCode.NotEmptyValidator, error.ErrorCode);
        }
       
    }
}
