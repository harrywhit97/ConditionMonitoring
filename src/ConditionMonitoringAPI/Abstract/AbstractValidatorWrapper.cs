using FluentValidation;
using FluentValidation.Results;

namespace ConditionMonitoringAPI.Abstract
{
    public abstract class AbstractValidatorWrapper<T> : AbstractValidator<T>
    {

        public AbstractValidatorWrapper()
            :base()
        {
        }

        public new virtual ValidationResult Validate(T instance)
        {
            return base.Validate(instance);
        }
    }
}
