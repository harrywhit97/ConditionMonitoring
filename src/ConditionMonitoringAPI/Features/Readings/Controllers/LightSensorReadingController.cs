using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Features.Sensors.Validators;
using Domain.Models;

namespace ConditionMonitoringAPI.Features.Readings.Controllers
{
    public class LightSensorReadingController : GenericController<LightSensorReading, long, LightSensorReadingValidator>
    {
        public LightSensorReadingController(ConditionMonitoringDbContext context, LightSensorReadingValidator validator)
            :base(context, validator)
        {
        }
    }
}
