using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Features.SensorsReadings.Validators;
using Domain.Models;
using MediatR;

namespace ConditionMonitoringAPI.Features.Readings.Controllers
{
    public class LightSensorReadingController : GenericController<LightSensorReading, long, LightSensorReadingValidator>
    {
        public LightSensorReadingController(ConditionMonitoringDbContext context, LightSensorReadingValidator validator, IMediator mediator)
            :base(context, validator, mediator)
        {
        }
    }
}
