using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Validator.BusinessDomain;
using CleanEvoBPM.Test.Application.Constants;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace CleanEvoBPM.Test.Application.Validator.BusinessDomain
{
    public class CreateBusinessDomainCommandValidatorTest
    {

        private CreateBusinessDomainCommandValidator _createBusinessDomainCommandValidator;
        private Mock<IGenericDataService<CreateBusinessDomainCommand>> _mockGenericDataService;
        public CreateBusinessDomainCommandValidatorTest()
        {
            var listBizs = new List<CreateBusinessDomainCommand>
            {
                new CreateBusinessDomainCommand
                {
                    BusinessDomainName = "Name"
                }
            }.AsEnumerable();
            _mockGenericDataService = new Mock<IGenericDataService<CreateBusinessDomainCommand>>();
            _mockGenericDataService.Setup(x => x.GetAll("BusinessDomain")).Returns(Task.FromResult(listBizs));
            _createBusinessDomainCommandValidator = new CreateBusinessDomainCommandValidator(_mockGenericDataService.Object);
        }

        [Fact]
        public void BusinessDomainName_IsNotEmpty()
        {
            var businessDomain = new CreateBusinessDomainCommand { BusinessDomainName = string.Empty, Description = "Description" };
            var result = _createBusinessDomainCommandValidator.TestValidate(businessDomain);
            result.ShouldHaveValidationErrorFor(t => t.BusinessDomainName);
            var error = result.Errors.FirstOrDefault();
            Assert.Equal(FluentValidationErrorCode.NotEmptyValidator, error.ErrorCode);
        }

        [Fact]
        public void BusinessDomainName_MaxLength250()
        {
            var businessDomain = new CreateBusinessDomainCommand
            {
                BusinessDomainName = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
                    + "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
                    + "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz",
                Description = "Description"
            };
            var result = _createBusinessDomainCommandValidator.TestValidate(businessDomain);
            // result.ShouldHaveValidationErrorFor(t => t.BusinessDomainName);
            var error = result.Errors.FirstOrDefault();
            Assert.Equal(FluentValidationErrorCode.MaximumLengthValidator, error.ErrorCode);
        }

        [Fact]
        public void BusinessDomainName_Unique()
        {

            var businessDomain = new CreateBusinessDomainCommand
            {
                BusinessDomainName = "Name",
                Description = "Description"
            };

            var result = _createBusinessDomainCommandValidator.TestValidate(businessDomain);
            result.ShouldHaveValidationErrorFor(t => t.BusinessDomainName);
            var error = result.Errors.FirstOrDefault();
        }

    }
}