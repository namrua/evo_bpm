using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Test.Application.Constants
{
    public static class FluentValidationErrorCode
    {
        public const string NotEmptyValidator = "NotEmptyValidator";
        public const string MaximumLengthValidator = "MaximumLengthValidator";
        public const string MinimumLengthValidator = "MinimumLengthValidator";
    }
}
