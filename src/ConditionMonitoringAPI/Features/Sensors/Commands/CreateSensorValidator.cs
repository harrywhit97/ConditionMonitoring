using FluentValidation;

namespace ConditionMonitoringAPI.Features.Sensors.Commands
{
    public class CreateSensorValidator : AbstractValidator<CreateSensor>
    {
        public CreateSensorValidator()
        {
            RuleFor(x => x.Name)
                .Length(1, 64);

            RuleFor(x => x.Pin)
                .GreaterThan(0);

            //Todo add more
        }
    }
}
