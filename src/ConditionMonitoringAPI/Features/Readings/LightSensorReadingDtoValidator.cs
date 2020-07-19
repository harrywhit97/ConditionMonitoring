using FluentValidation;
using WebApiUtilities.Abstract;

namespace ConditionMonitoringAPI.Features.Readings
{
    public class LightSensorReadingDtoValidator<TDto> : DtoValidator<TDto, LightSensorReadingDto>
        where TDto : LightSensorReadingDto
    {
        public LightSensorReadingDtoValidator()
        {
            RuleFor(x => x.RawReading)
                .GreaterThan(0);

            //Todo add more
        }
    }
}
