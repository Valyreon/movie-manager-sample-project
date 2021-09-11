using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ServiceLayer.Attributes
{
    public class AllowedStringValuesAttribute : ValidationAttribute
    {
        private readonly string[] allowedValues;

        public AllowedStringValuesAttribute(params string[] values)
        {
            allowedValues = values;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string str = (value as string).ToLower();

            if(!allowedValues.Contains(str))
            {
                return new ValidationResult($"'{str}' is not an allowed value.");
            }

            return ValidationResult.Success;
        }
    }
}
