using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using FluentValidation;
using FluentValidation.Validators;

namespace CleanEvoBPM.Application.Validator.CustomValidator
{
    public class UniqueValidator<T> : PropertyValidator where T : class
    {
        private readonly IEnumerable<T> _item;
        public UniqueValidator(IEnumerable<T> item) : base("{PropertyName} must be unique")
        {
            _item = item;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var editedItem = context.Instance as T;
            var newValue = context.PropertyValue as string;
            newValue = newValue.Trim();
            var property = typeof(T).GetTypeInfo().GetDeclaredProperty(context.PropertyName);

            return _item.All(item => item.Equals(editedItem) || property.GetValue(item).ToString() != newValue);
        }

        
    }
}