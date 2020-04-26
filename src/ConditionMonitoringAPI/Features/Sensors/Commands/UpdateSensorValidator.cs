using FluentValidation;

namespace ConditionMonitoringAPI.Features.Sensors.Commands
{
    public class UpdateSensorValidator : AbstractValidator<UpdateSensor>
    {
        public UpdateSensorValidator()
        {
            RuleFor(x => x.Name)
                .Length(1, 64);

            RuleFor(x => x.Pin)
                .GreaterThan(1);
        }
    }
}
