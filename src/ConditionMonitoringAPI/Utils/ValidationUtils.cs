using ConditionMonitoringAPI.Abstract;
using Domain.Interfaces;
using FluentValidation;
using System;

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

        internal static void ValidateEntity<TValidator, T, TId>(TValidator validator, T entity)
            where TValidator : AbstractValidatorWrapper<T>
            where T : class, IHasId<TId>
        {
            throw new NotImplementedException();
        }
    }
}
