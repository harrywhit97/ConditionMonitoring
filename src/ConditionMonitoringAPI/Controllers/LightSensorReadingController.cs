using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Validators;
using Domain.Models;

namespace ConditionMonitoringAPI.Controllers
{
    public class LightSensorReadingController : GenericController<LightSensorReading, long, LightSensorReadingValidator>
    {
        public LightSensorReadingController(ConditionMonitoringDbContext context, LightSensorReadingValidator validator)
            :base(context, validator)
        {
        }
    }
}
