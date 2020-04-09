using ConditionMonitoringAPI.Abstract;
using FluentValidation;

namespace ConditionMonitoringAPI.Utils
{
    public static class ValidationUtils
    {
        public static void ValidateEntity<T>(AbstractValidatorWrapper<T> validator, T entity)
        {
            var result = validator.Validate(entity);
            
            if (!result.IsValid)
                throw new ValidationException(result.ToString());
        }
    }
}
