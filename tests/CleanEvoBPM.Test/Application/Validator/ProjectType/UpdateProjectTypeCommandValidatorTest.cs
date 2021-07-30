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
    public class UpdateProjectTypeCommandValidatorTest
    {
        private UpdateProjectTypeCommandValidator _updateProjectTypeCommandValidator;
        private readonly Mock<IGenericDataService<UpdateProjectTypeCommand>> _mockUpdateProjectTypeCommand;
        public UpdateProjectTypeCommandValidatorTest()
        {
            _mockUpdateProjectTypeCommand = new Mock<IGenericDataService<UpdateProjectTypeCommand>>();
            _updateProjectTypeCommandValidator = new UpdateProjectTypeCommandValidator(_mockUpdateProjectTypeCommand.Object);
        }

        [Fact]
        public void ProjectTypeName_IsNotEmpty()
        {
            var projectType = new UpdateProjectTypeCommand {ProjectTypeName=string.Empty, ProjectTypeDescription = "Description" };
            var result =_updateProjectTypeCommandValidator.TestValidate(projectType);
            result.ShouldHaveValidationErrorFor(t => t.ProjectTypeName);
            var error = result.Errors.Where(p=>p.ErrorCode==FluentValidationErrorCode.NotEmptyValidator).FirstOrDefault();
            Assert.NotNull(error);
        }

        [Fact]
        public void ProjectTypeName_MaxLength250()
        {
            var projectType = new UpdateProjectTypeCommand
            { 
                ProjectTypeName = "Virtual Machine give you full control over the envi Virtual Machine give you full control over the envi Virtual Machine give you full control over the envi Virtual Machine give you full control over the envi Virtual Machine give you full control over the envi" , 
                ProjectTypeDescription = "Description" 
            };
            var result = _updateProjectTypeCommandValidator.TestValidate(projectType);
            result.ShouldHaveValidationErrorFor(t => t.ProjectTypeName);
            var error = result.Errors.Where(p=>p.ErrorCode==FluentValidationErrorCode.MaximumLengthValidator).FirstOrDefault();
            Assert.NotNull(error);
        }        

        [Fact]
        public void ProjectTypeDescription_MaxLength1000()
        {
            var projectType = new UpdateProjectTypeCommand
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
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
            };
            var result = _updateProjectTypeCommandValidator.TestValidate(projectType);
            result.ShouldHaveValidationErrorFor(t => t.ProjectTypeDescription);
            var error = result.Errors.Where(p=>p.ErrorCode==FluentValidationErrorCode.MaximumLengthValidator).FirstOrDefault();
            Assert.NotNull(error);
        }
    }
}
