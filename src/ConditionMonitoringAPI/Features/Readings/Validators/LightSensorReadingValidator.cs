using ConditionMonitoringAPI.Abstract;
using Domain.Models;
using FluentValidation;
using System;

namespace ConditionMonitoringAPI.Features.SensorsReadings.Validators
{
    public class LightSensorReadingValidator : AbstractValidatorWrapper<LightSensorReading>
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
