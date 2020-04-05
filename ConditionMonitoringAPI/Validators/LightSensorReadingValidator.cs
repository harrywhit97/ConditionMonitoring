using Domain.Models;
using FluentValidation;

namespace ConditionMonitoringAPI.Validators
{
    public class LightSensorReadingValidator : AbstractValidator<LightSensorReading>
    {
        public LightSensorReadingValidator()
        {
            RuleFor(x => x.RawReading)
                .GreaterThanOrEqualTo(0);
        }
    }
}
