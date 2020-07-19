using FluentValidation;
using WebApiUtilities.Abstract;

namespace ConditionMonitoringAPI.Features.Sensors
{
    public class SensorValidator<TDto> : DtoValidator<TDto, SensorDto>
        where TDto : SensorDto
    {
        public SensorValidator()
        {
            RuleFor(x => x.Name)
                .Length(1, 64);

            RuleFor(x => x.Pin)
                .GreaterThan(1);
        }
    }
}
