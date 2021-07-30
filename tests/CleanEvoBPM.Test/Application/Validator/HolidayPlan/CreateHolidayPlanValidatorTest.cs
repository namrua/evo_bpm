using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Command;
using CleanEvoBPM.Application.Validator.HolidayPlan;
using CleanEvoBPM.Test.Application.Constants;
using FluentValidation.TestHelper;
using System;
using System.Linq;
using Xunit;

namespace CleanEvoBPM.Test.Application.Validator.HolidayPlan
{
    public class CreateHolidayPlanValidatorTest
    {
        private CreateProjectHolidayPlanCommandValidator _validator;

        public CreateHolidayPlanValidatorTest()
        {
            _validator = new CreateProjectHolidayPlanCommandValidator();
        }

        [Theory]
        [MemberData(nameof(CommandProjectEmpty))]
        public void CreatePlanValidator_Failure_ProjectEmpty(CreateProjectHolidayPlanCommand command)
        {
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(t => t.ProjectId);

            var error = result.Errors.FirstOrDefault(x =>
                x.PropertyName == "ProjectId" &&
                x.ErrorCode == FluentValidationErrorCode.NotEmptyValidator);
            Assert.NotNull(error);
        }

        [Theory]
        [MemberData(nameof(CommandResourceEmpty))]
        public void CreatePlanValidator_Failure_ResourceEmpty(CreateProjectHolidayPlanCommand command)
        {
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(t => t.ResourceId);

            var error = result.Errors.FirstOrDefault(x =>
                x.PropertyName == "ResourceId" &&
                x.ErrorCode == FluentValidationErrorCode.NotEmptyValidator);
            Assert.NotNull(error);
        }

        [Theory]
        [MemberData(nameof(CommandRoleEmpty))]
        public void CreatePlanValidator_Failure_RoleEmpty(CreateProjectHolidayPlanCommand command)
        {
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(t => t.ResourceRoleId);

            var error = result.Errors.FirstOrDefault(x =>
                x.PropertyName == "ResourceRoleId" &&
                x.ErrorCode == FluentValidationErrorCode.NotEmptyValidator);
            Assert.NotNull(error);
        }

        [Theory]
        [MemberData(nameof(CommandFromDateEmpty))]
        public void CreatePlanValidator_Failure_FromDateEmpty(CreateProjectHolidayPlanCommand command)
        {
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(t => t.FromDate);

            var error = result.Errors.FirstOrDefault(x =>
                x.PropertyName == "FromDate");
            Assert.NotNull(error);
        }

        [Theory]
        [MemberData(nameof(CommandToDateEmpty))]
        public void CreatePlanValidator_Failure_ToDateEmpty(CreateProjectHolidayPlanCommand command)
        {
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(t => t.ToDate);

            var error = result.Errors.FirstOrDefault(x =>
                x.PropertyName == "ToDate");
            Assert.NotNull(error);
        }

        [Theory]
        [MemberData(nameof(CommandNoteTooLong))]
        public void CreatePlanValidator_Failure_NoteTooLong(CreateProjectHolidayPlanCommand command)
        {
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(t => t.Note);

            var error = result.Errors.FirstOrDefault(x =>
                x.PropertyName == "Note" &&
                x.ErrorCode == FluentValidationErrorCode.MaximumLengthValidator);
            Assert.NotNull(error);
        }

        [Theory]
        [MemberData(nameof(CommandToDateSmallerThanFromDate))]
        public void CreatePlanValidator_Failure_ToDateSmallerThanFromDate(CreateProjectHolidayPlanCommand command)
        {
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(t => t.ToDate);

            var error = result.Errors.FirstOrDefault(x =>
                x.PropertyName == "ToDate" && x.ErrorCode == "GreaterThanOrEqualValidator");
            Assert.NotNull(error);
        }

        [Theory]
        [MemberData(nameof(CommandValid))]
        public void CreatePlanValidator_Success(CreateProjectHolidayPlanCommand command)
        {
            var result = _validator.TestValidate(command);
            Assert.True(result.IsValid);
        }

        public static TheoryData<CreateProjectHolidayPlanCommand> CommandProjectEmpty = new TheoryData<CreateProjectHolidayPlanCommand>
        {
            new CreateProjectHolidayPlanCommand()
        };

        public static TheoryData<CreateProjectHolidayPlanCommand> CommandResourceEmpty = new TheoryData<CreateProjectHolidayPlanCommand>
        {
            new CreateProjectHolidayPlanCommand()
        };

        public static TheoryData<CreateProjectHolidayPlanCommand> CommandRoleEmpty = new TheoryData<CreateProjectHolidayPlanCommand>
        {
            new CreateProjectHolidayPlanCommand()
        };

        public static TheoryData<CreateProjectHolidayPlanCommand> CommandFromDateEmpty = new TheoryData<CreateProjectHolidayPlanCommand>
        {
            new CreateProjectHolidayPlanCommand()
        };

        public static TheoryData<CreateProjectHolidayPlanCommand> CommandToDateEmpty = new TheoryData<CreateProjectHolidayPlanCommand>
        {
            new CreateProjectHolidayPlanCommand()
        };

        public static TheoryData<CreateProjectHolidayPlanCommand> CommandToDateSmallerThanFromDate = new TheoryData<CreateProjectHolidayPlanCommand>
        {
            new CreateProjectHolidayPlanCommand
            {
                ProjectId = Guid.NewGuid(),
                ResourceId = Guid.NewGuid(),
                ResourceRoleId = Guid.NewGuid(),
                FromDate = DateTime.Now,
                ToDate = DateTime.Now.AddMilliseconds(-1)
            }
        };

        public static TheoryData<CreateProjectHolidayPlanCommand> CommandNoteTooLong = new TheoryData<CreateProjectHolidayPlanCommand>
        {
            new CreateProjectHolidayPlanCommand
            {
                Note = @"v5SSt3JDBz9UKyVqlhma7USdJVQdwjYZpB176EBPqr68i7dCKW0Kp01HFAiIBE7nbKmjJzURw
                        CIjeIGfOnAJGzZwT5FQusyfGqKy9qNYDKP8Q4giIREGDYt06MQ4e1U6AQvqBZW2K9eB1CgDBx
                        vcb9Mt8Mq8X7cwGMrvo2GDoGTd0CX4Zfupga8Fd79ycIjSoKJnHLqlRXtr321jfSutPbVzkd5
                        1cA0EtcwgICixldoxeEjt8YdxfECtOJUO7925HOG1a6n5YAhXiXageamjJxN3P4BuG0gXWhgR
                        QXA1SBs09i77PpNYuNqHbXKeZdZtkPHHfg26WONGrd00emm1fiT5okHuoLE3iQN97V5DvoDudz
                        gLJkBpnPqJczF7lATGqgYzANeIcIPmb0HfO1JHXLqcaLOIScIo2ijwoenGWV0ayX3YJPNJiNVh
                        FBpRBHqN1TYWfP2QV6hCsseqNNmeEOc2JhIGmt8lC8seFjDMZyQ7Sl1xq9hRAO6BbYqdJEEJCal
                        42ofo26ht4j3jQVC6LRsANHUPwds1a3agOsaUOAT1bQe3gjRqi0SzFTx96rJajp5vtN373OHKdR
                        Z2a6lApBIze30dFAEhlWoVGK0gJlRnCNWtAqtRuTwurCsLO8DR4BDRnqiHYRUgRmrM7LbtwLToL
                        sIlESFsQ9BgMCK3ujkjShmbWBwxmurP5CNTMd77LG8qkKKAw0qyoDLe5mp4Idpo4sLBwT4oHmrU
                        ckBgA4gY0O1R2jDmm9d5saeZuZqtcnNXyabn4c9hgLLx7KdxsnXAoyEAw3su5eozlZrowVGkQCx
                        qpV5c0mplOynovajJQshnAXM5guWVxsxvkBbS8RSjLuq0p3AtFu1RDT2UceIz2xyKHh3BWk90rD
                        39VJF4hou1rr2ULvnMMgW4OrSLHDF2giNtfLEMwwAo2wioAtzShLsVAobmhV9P8mbItwG8KCwb7
                        72Jq3zDRLpcPnTLvnCrBEfDRs5CDVhV4PRna"
            }
        };

        public static TheoryData<CreateProjectHolidayPlanCommand> CommandValid = new TheoryData<CreateProjectHolidayPlanCommand>
        {
            new CreateProjectHolidayPlanCommand
            {
                ProjectId = Guid.NewGuid(),
                ResourceId = Guid.NewGuid(),
                ResourceRoleId = Guid.NewGuid(),
                FromDate = DateTime.Now,
                ToDate = DateTime.Now,
            },
            new CreateProjectHolidayPlanCommand
            {
                ProjectId = Guid.NewGuid(),
                ResourceId = Guid.NewGuid(),
                ResourceRoleId = Guid.NewGuid(),
                FromDate = DateTime.Now,
                ToDate = DateTime.Now.AddMilliseconds(1),
                // note length 1000
                Note = "3zgJOeDCCSLtSVIvpZYWA3l24GpJlz1mHBI76TgjOJMZG6pWygvneLvIAqPpV2frmkKrr75AldxT5nzsngml4eNnih1qMfJW6cNg2U3bNZqbl3ALqjICrIStuxqpV5pW9SHF2zXRKrOfChpa5OMH0zEeHhrNcQZ4s8sXemRFzC4EpvDht1DaVt0B2QVZvzhjQtP5eMzPN08Qfi43MAGWMOohdV3KrgADvrniu362gy4iEQiPPWuw7pxkvzKm0RA2s1DLG5MxxUqeHZo6fcr9l1bFrLyw7VWc3pqrlGlxXFlhVgDLshLtWBvjCZmE5yLZjDiS5Wzk20NNrAh81VL1w8uJkrDRgMxYQIwTcbjxrxIPtGrZoRM2wjvJcDoBOE0nlTzqJKa1FcVd7BIg1ZNjV6fc2UpfoOHMZkyYd2WMIPdoKgEceeAABDhW8lK8LBju39dhIij5FzlEnNZLqGofgTyRZXqx2C9w8KAJPj6bvFLf7uT76YeuOJviHaPv2R31ogiTkx0hzSvyR84TxJDQHBJfVl2TBiGlEMOaaPEq0J7RwMxSvmSZbVprcg5g2RusEIFffJM6vKJzmrvVfQ299kFZIJgLWZneJaaFrKjDGNONw6BPOI23LhzJjuQYhcfgoXOWhwMlaHQVVS4ivcQ3Pc9OHlvKNiwQMQOKnCZlJAa74vxkDaPZzyzjyzFaqVgxxti53FHChcqo1AyX69FcrgzLJIC5f3235scnBH5c3893FnijSlgjYF8XRYBgiKvu6m2IPYlFJTn4W69QFhYD19djXPRvmZMXUS6r1gDJ1Lrqh4iIHunfEjvGY66rH0ZNzmPfh0hv3pnwcDbQJm1fKClSwf9fgog3iJyunEixNCUd0FnLHufLTVBD14UT4ZCsnBKBJVlL08OHc7gh33dFKqQ8oRgJ1QbAZkxSrKLiubZOai4WXkjHFTbz4DGxnWKRTvRjksO0SLarc8k65Z5PaAWWP21gFu46SUssd7sr",
            }
        };
    }
}
