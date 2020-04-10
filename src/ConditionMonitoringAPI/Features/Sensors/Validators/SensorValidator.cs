using ConditionMonitoringAPI.Abstract;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;

namespace ConditionMonitoringAPI.Features.Sensors
{
    public class SensorValidator : AbstractValidatorWrapper<Sensor<ISensorReading>>
    {
        public SensorValidator()
        {
            RuleFor(x => x.Pin)
                .GreaterThanOrEqualTo(0);
        }
    }
}
