using CleanEvoBPM.Application.CQRS.Methodology.Command;
using CleanEvoBPM.Application.Validator.Methodology;
using CleanEvoBPM.Test.Application.Constants;
using FluentValidation.TestHelper;
using System.Linq;
using Xunit;

namespace CleanEvoBPM.Test.Application.Validator.Methodology
{
    public class UpdateMethodologyValidatorTest
    {
        private UpdateMethodologyCommandValidator _validator;

        public UpdateMethodologyValidatorTest()
        {
            _validator = new UpdateMethodologyCommandValidator();
        }

        [Theory]
        [MemberData(nameof(CommandWithNameEmpty))]
        public void UpdateMethodologyValidator_Failure_NameEmpty(UpdateMethodologyCommand command)
        {
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(t => t.MethodologyName);

            var error = result.Errors.FirstOrDefault(x =>
                x.PropertyName == "MethodologyName" &&
                x.ErrorCode == FluentValidationErrorCode.NotEmptyValidator);
            Assert.NotNull(error);
        }

        [Theory]
        [MemberData(nameof(CommandWithNameTooShort))]
        public void UpdateMethodologyValidator_Failure_NameTooShort(UpdateMethodologyCommand command)
        {
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(t => t.MethodologyName);

            var error = result.Errors.FirstOrDefault(x =>
                x.PropertyName == "MethodologyName" &&
                x.ErrorCode == FluentValidationErrorCode.MinimumLengthValidator);
            Assert.NotNull(error);
        }

        [Theory]
        [MemberData(nameof(CommandWithNameTooLong))]
        public void UpdateMethodologyValidator_Failure_NameTooLong(UpdateMethodologyCommand command)
        {
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(t => t.MethodologyName);

            var error = result.Errors.FirstOrDefault(x =>
                x.PropertyName == "MethodologyName" &&
                x.ErrorCode == FluentValidationErrorCode.MaximumLengthValidator);
            Assert.NotNull(error);
        }

        [Theory]
        [MemberData(nameof(CommandWithDescriptionTooLong))]
        public void UpdateMethodologyValidator_Failure_DescriptionTooLong(UpdateMethodologyCommand command)
        {
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(t => t.Description);

            var error = result.Errors.FirstOrDefault(x =>
                x.PropertyName == "Description" &&
                x.ErrorCode == FluentValidationErrorCode.MaximumLengthValidator);
            Assert.NotNull(error);
        }

        [Theory]
        [MemberData(nameof(CommandValid))]
        public void UpdateMethodologyValidator_Success(UpdateMethodologyCommand command)
        {
            var result = _validator.TestValidate(command);
            Assert.True(result.IsValid);
        }

        public static TheoryData<UpdateMethodologyCommand> CommandWithNameEmpty = new TheoryData<UpdateMethodologyCommand>
        {
            new UpdateMethodologyCommand(),
            new UpdateMethodologyCommand
            {
                MethodologyName = "",
            }
        };

        public static TheoryData<UpdateMethodologyCommand> CommandWithNameTooShort = new TheoryData<UpdateMethodologyCommand>
        {
            new UpdateMethodologyCommand
            {
                MethodologyName = "a",
            },
            new UpdateMethodologyCommand
            {
                MethodologyName = "abcd",
            }
        };

        public static TheoryData<UpdateMethodologyCommand> CommandWithNameTooLong = new TheoryData<UpdateMethodologyCommand>
        {
            new UpdateMethodologyCommand
            {
                MethodologyName = @"1rQrLyL4EkLWsTrgeusWRgpcMZXfyYkRzdJyVKFgbQjh07gfr4
                                    euDtWkfWEgovQU3oNHYLO4FIxf6BDBDWaXtw7Z88cZ8bk4UZ1mz",
            }
        };

        public static TheoryData<UpdateMethodologyCommand> CommandWithDescriptionTooLong = new TheoryData<UpdateMethodologyCommand>
        {
            new UpdateMethodologyCommand
            {
                Description = @"v5SSt3JDBz9UKyVqlhma7USdJVQdwjYZpB176EBPqr68i7dCKW0Kp01HFAiIBE7nbKmjJzURw
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

        public static TheoryData<UpdateMethodologyCommand> CommandValid = new TheoryData<UpdateMethodologyCommand>
        {
            // name length 5
            new UpdateMethodologyCommand
            {
                MethodologyName = "ABCDE"
            },
            new UpdateMethodologyCommand
            {
                MethodologyName = "ABCDE",
                Description = "Description"
            },
            // name length 100
            new UpdateMethodologyCommand
            {
                MethodologyName = "v1dTTYBcz4e4644EzKQOqKMqHBkHznKzrtNLfjx4kObgBkfj92aJ1ZYGaQsVMVphra158KxV014NHbkQaY66toA9wpDiT9JLjsDH",
            },
            // description length 1000
            new UpdateMethodologyCommand
            {
                MethodologyName = "Some name",
                Description = "3zgJOeDCCSLtSVIvpZYWA3l24GpJlz1mHBI76TgjOJMZG6pWygvneLvIAqPpV2frmkKrr75AldxT5nzsngml4eNnih1qMfJW6cNg2U3bNZqbl3ALqjICrIStuxqpV5pW9SHF2zXRKrOfChpa5OMH0zEeHhrNcQZ4s8sXemRFzC4EpvDht1DaVt0B2QVZvzhjQtP5eMzPN08Qfi43MAGWMOohdV3KrgADvrniu362gy4iEQiPPWuw7pxkvzKm0RA2s1DLG5MxxUqeHZo6fcr9l1bFrLyw7VWc3pqrlGlxXFlhVgDLshLtWBvjCZmE5yLZjDiS5Wzk20NNrAh81VL1w8uJkrDRgMxYQIwTcbjxrxIPtGrZoRM2wjvJcDoBOE0nlTzqJKa1FcVd7BIg1ZNjV6fc2UpfoOHMZkyYd2WMIPdoKgEceeAABDhW8lK8LBju39dhIij5FzlEnNZLqGofgTyRZXqx2C9w8KAJPj6bvFLf7uT76YeuOJviHaPv2R31ogiTkx0hzSvyR84TxJDQHBJfVl2TBiGlEMOaaPEq0J7RwMxSvmSZbVprcg5g2RusEIFffJM6vKJzmrvVfQ299kFZIJgLWZneJaaFrKjDGNONw6BPOI23LhzJjuQYhcfgoXOWhwMlaHQVVS4ivcQ3Pc9OHlvKNiwQMQOKnCZlJAa74vxkDaPZzyzjyzFaqVgxxti53FHChcqo1AyX69FcrgzLJIC5f3235scnBH5c3893FnijSlgjYF8XRYBgiKvu6m2IPYlFJTn4W69QFhYD19djXPRvmZMXUS6r1gDJ1Lrqh4iIHunfEjvGY66rH0ZNzmPfh0hv3pnwcDbQJm1fKClSwf9fgog3iJyunEixNCUd0FnLHufLTVBD14UT4ZCsnBKBJVlL08OHc7gh33dFKqQ8oRgJ1QbAZkxSrKLiubZOai4WXkjHFTbz4DGxnWKRTvRjksO0SLarc8k65Z5PaAWWP21gFu46SUssd7sr",
            }
        };
    }
}
