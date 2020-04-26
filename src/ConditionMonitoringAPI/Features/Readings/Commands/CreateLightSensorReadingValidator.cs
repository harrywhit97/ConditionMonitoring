using FluentValidation;

namespace ConditionMonitoringAPI.Features.Readings.Commands
{
    public class CreateLightSensorReadingValidator : AbstractValidator<CreateLightSensorReading>
    {
        public CreateLightSensorReadingValidator()
        {
            RuleFor(x => x.RawReading)
                .GreaterThan(0);

            //Todo add more
        }
    }
}
