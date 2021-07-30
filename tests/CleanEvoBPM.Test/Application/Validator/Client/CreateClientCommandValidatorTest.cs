using CleanEvoBPM.Application.CQRS.Client.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Validator.Client;
using CleanEvoBPM.Test.Application.Constants;
using FluentValidation.TestHelper;
using Moq;
using System.Linq;
using Xunit;

namespace CleanEvoBPM.Test.Application.Validator.Client
{
    public class CreateClientCommandValidatorTest
    {
        private CreateClientCommandValidator _createClientCommandValidator;
        private readonly Mock<IGenericDataService<CreateClientCommand>> _mockreateClientCommand;
        public CreateClientCommandValidatorTest()
        {
            _mockreateClientCommand = new Mock<IGenericDataService<CreateClientCommand>>();
            _createClientCommandValidator = new CreateClientCommandValidator(_mockreateClientCommand.Object);
        }

        [Fact]
        public void ClientName_IsNotEmpty()
        {
            var client = new CreateClientCommand {ClientName=string.Empty, ClientDivisionName = "Description" };
            var result =_createClientCommandValidator.TestValidate(client);
            result.ShouldHaveValidationErrorFor(t => t.ClientName);
            var error = result.Errors.Where(p=>p.ErrorCode==FluentValidationErrorCode.NotEmptyValidator).FirstOrDefault();
            Assert.NotNull(error);
        }

        [Fact]
        public void ClientName_MaxLength250()
        {
            var client = new CreateClientCommand { 
                ClientName = "Virtual Machine give you full control over the envi Virtual Machine give you full control over the envi Virtual Machine give you full control over the envi Virtual Machine give you full control over the envi Virtual Machine give you full control over the envi Virtual Machine give you full control over the envi Virtual Machine give you full control over the envi" , 
                ClientDivisionName = "Description" 
            };
            var result = _createClientCommandValidator.TestValidate(client);
            result.ShouldHaveValidationErrorFor(t => t.ClientName);
            var error = result.Errors.Where(p=>p.ErrorCode==FluentValidationErrorCode.MaximumLengthValidator).FirstOrDefault();
            Assert.NotNull(error);
        }        

        [Fact]
        public void ClientDescription_MaxLength1000()
        {
            var client = new CreateClientCommand
            {
                ClientName = "Azure",
                ClientDivisionName = "Virtual Machine give you full control over the environment. Container give you limited control. And Serverless computing does not allow you to do anything with infrastructure. Virtual Machine give you "
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
            var result = _createClientCommandValidator.TestValidate(client);
            result.ShouldHaveValidationErrorFor(t => t.ClientDivisionName);
            var error = result.Errors.Where(p=>p.ErrorCode==FluentValidationErrorCode.MaximumLengthValidator).FirstOrDefault();
            Assert.NotNull(error);
        }
    }
}
