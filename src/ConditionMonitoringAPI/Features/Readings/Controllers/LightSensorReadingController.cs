using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Features.Readings.Commands;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace ConditionMonitoringAPI.Features.Readings.Controllers
{
    public class LightSensorReadingController : AbstractController<LightSensorReading, long, CreateLightSensorReading, UpdateLightSensorReading>
    {
        public LightSensorReadingController(ConditionMonitoringDbContext context, 
            ILogger<LightSensorReadingController> logger)
            :base(context, logger)
        {
        }
    }
}
