using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;
using WebApiUtilities.Abstract;

namespace ConditionMonitoringAPI.Features.Sensors
{
    public class SensorController : CrudController<Sensor<ISensorReading>, long, CreateSensor, UpdateSensor>
    {
        public SensorController(ConditionMonitoringDbContext context,
            ILogger<SensorController> logger)
            :base(context, logger)
        {
        }
    }
}
