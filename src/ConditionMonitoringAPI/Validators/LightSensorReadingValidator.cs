using Domain.Models;
using FluentValidation;
using System;

namespace ConditionMonitoringAPI.Validators
{
    public class LightSensorReadingValidator : AbstractValidator<LightSensorReading>
    {
        public LightSensorReadingValidator()
        {
            RuleFor(x => x.RawReading)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.ReadingTime)
                .GreaterThan(DateTimeOffset.MinValue);
        }
    }
}
