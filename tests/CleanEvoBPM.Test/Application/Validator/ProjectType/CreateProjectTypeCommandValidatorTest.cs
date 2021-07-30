using CleanEvoBPM.Application.CQRS.ProjectType.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Validator;
using CleanEvoBPM.Test.Application.Constants;
using FluentValidation.TestHelper;
using Moq;
using System.Linq;
using Xunit;

namespace CleanEvoBPM.Test.Application.Validator.ProjectType
{
    public class CreateProjectTypeCommandValidatorTest
    {
        private CreateProjectTypeCommandValidator _createProjectTypeCommandValidator;
        private readonly Mock<IGenericDataService<CreateProjectTypeCommand>> _mockCreateProjectTypeCommand;
        public CreateProjectTypeCommandValidatorTest()
        {
            _mockCreateProjectTypeCommand = new Mock<IGenericDataService<CreateProjectTypeCommand>>();
            _createProjectTypeCommandValidator = new CreateProjectTypeCommandValidator(_mockCreateProjectTypeCommand.Object);
        }

        [Fact]
        public void ProjectTypeName_IsNotEmpty()
        {
            var projectType = new CreateProjectTypeCommand {ProjectTypeName=string.Empty, ProjectTypeDescription = "Description" };
            var result =_createProjectTypeCommandValidator.TestValidate(projectType);
            result.ShouldHaveValidationErrorFor(t => t.ProjectTypeName);
            var error = result.Errors.Where(p=>p.ErrorCode==FluentValidationErrorCode.NotEmptyValidator).FirstOrDefault();
            Assert.NotNull(error);
        }

        [Fact]
        public void ProjectTypeName_MaxLength250()
        {
            var projectType = new CreateProjectTypeCommand { 
                ProjectTypeName = "Virtual Machine give you full control over the enviVirtual Machine give you full control over the enviVirtual Machine give you full control over the enviVirtual Machine give you full control over the enviVirtual Machine give you full control over the envi" , 
                ProjectTypeDescription = "Description" 
            };
            var result = _createProjectTypeCommandValidator.TestValidate(projectType);
            result.ShouldHaveValidationErrorFor(t => t.ProjectTypeName);
            var error = result.Errors.Where(p=>p.ErrorCode==FluentValidationErrorCode.MaximumLengthValidator).FirstOrDefault();
            Assert.NotNull(error);
        }        

        [Fact]
        public void ProjectTypeDescription_MaxLength1000()
        {
            var projectType = new CreateProjectTypeCommand
            {
                ProjectTypeName = "Azure",
                ProjectTypeDescription = "Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
            };
            var result = _createProjectTypeCommandValidator.TestValidate(projectType);
            result.ShouldHaveValidationErrorFor(t => t.ProjectTypeDescription);
            var error = result.Errors.Where(p=>p.ErrorCode==FluentValidationErrorCode.MaximumLengthValidator).FirstOrDefault();
            Assert.NotNull(error);
        }
    }
}
