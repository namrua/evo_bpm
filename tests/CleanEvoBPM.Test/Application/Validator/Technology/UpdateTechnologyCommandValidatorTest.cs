using CleanEvoBPM.Application.CQRS.Technology.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Validator.Technology;
using CleanEvoBPM.Test.Application.Constants;
using FluentValidation.TestHelper;
using Moq;
using System.Linq;
using Xunit;

namespace CleanEvoBPM.Test.Application.Validator.Technology
{
    public class UpdateTechnologyCommandValidatorTest
    {
        private UpdateTechnologyCommandValidator _updateTechnologyCommandValidator;
        private readonly Mock<IGenericDataService<UpdateTechnologyCommand>> _mockUpdateTechnologyCommand;
        public UpdateTechnologyCommandValidatorTest()
        {
            _mockUpdateTechnologyCommand = new Mock<IGenericDataService<UpdateTechnologyCommand>>();
            _updateTechnologyCommandValidator = new UpdateTechnologyCommandValidator(_mockUpdateTechnologyCommand.Object);
        }

        [Fact]
        public void TechnologyName_IsNotEmpty()
        {
            var technology = new UpdateTechnologyCommand {TechnologyName=string.Empty, TechnologyDescription = "Description" };
            var result =_updateTechnologyCommandValidator.TestValidate(technology);
            result.ShouldHaveValidationErrorFor(t => t.TechnologyName);
            var error = result.Errors.Where(p=>p.ErrorCode==FluentValidationErrorCode.NotEmptyValidator).FirstOrDefault();
            Assert.NotNull(error);
        }

        [Fact]
        public void TechnologyName_MaxLength250()
        {
            var technology = new UpdateTechnologyCommand
            { 
                TechnologyName = "Virtual Machine give you full control over the envi Virtual Machine give you full control over the envi Virtual Machine give you full control over the envi Virtual Machine give you full control over the envi Virtual Machine give you full control over the envi" , 
                TechnologyDescription = "Description" 
            };
            var result = _updateTechnologyCommandValidator.TestValidate(technology);
            result.ShouldHaveValidationErrorFor(t => t.TechnologyName);
            var error = result.Errors.Where(p=>p.ErrorCode==FluentValidationErrorCode.MaximumLengthValidator).FirstOrDefault();
            Assert.NotNull(error);
        }        

        [Fact]
        public void TechnologyDescription_MaxLength1000()
        {
            var technology = new UpdateTechnologyCommand
            {
                TechnologyName = "Azure",
                TechnologyDescription = "Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
                +"Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
            };
            var result = _updateTechnologyCommandValidator.TestValidate(technology);
            result.ShouldHaveValidationErrorFor(t => t.TechnologyDescription);
            var error = result.Errors.Where(p=>p.ErrorCode==FluentValidationErrorCode.MaximumLengthValidator).FirstOrDefault();
            Assert.NotNull(error);
        }
    }
}
