using FluentValidation;

namespace ConditionMonitoringAPI.Features.Readings.Commands
{
    public class UpdateLightSensorReadingValidator : AbstractValidator<UpdateLightSensorReading>
    {
        public UpdateLightSensorReadingValidator()
        {
            RuleFor(x => x.RawReading)
                .GreaterThan(0);
        }
    }
}
