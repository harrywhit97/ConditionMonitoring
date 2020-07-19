using Domain.Models;
using Microsoft.Extensions.Logging;
using WebApiUtilities.Abstract;

namespace ConditionMonitoringAPI.Features.Readings
{
    public class LightSensorReadingController : CrudController<LightSensorReading, long, CreateLightSensorReading, UpdateLightSensorReading>
    {
        public LightSensorReadingController(ConditionMonitoringDbContext context, 
            ILogger<LightSensorReadingController> logger)
            :base(context, logger)
        {
        }
    }
}
