using System.Collections.Generic;
using FluentValidation;

namespace CleanEvoBPM.Application.Validator.CustomValidator
{
    public static class CustomValidator
    {
        public static IRuleBuilderOptions<TItem, TProperty> IsUnique<TItem, TProperty>(
                this IRuleBuilder<TItem, TProperty> ruleBuilder, IEnumerable<TItem> items)
                where TItem : class
        {
            return ruleBuilder.SetValidator(new UniqueValidator<TItem>(items));
        }
    }
}