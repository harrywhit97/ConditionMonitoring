using ConditionMonitoringAPI.Abstract;
using FluentValidation.Results;
using System;
using System.Linq;
using System.Text;

namespace ConditionMonitoringAPI.Utils
{
    public static class ValidationUtils
    {
        public static void ValidateEntity<T>(AbstractValidatorWrapper<T> validator, T entity)
        {
            var result = validator.Validate(entity);
            
            if (!result.IsValid)
                throw new ArgumentException(GetValidationErrorString(result));
        }

        public static string GetValidationErrorString(ValidationResult result)
        {
            var errors = new StringBuilder();
            result.Errors.Select(x => errors.Append($"{x.ErrorMessage};"));
            return errors.ToString();
        }
    }
}
