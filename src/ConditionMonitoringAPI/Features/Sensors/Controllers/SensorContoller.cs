using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Features.Sensors.Commands;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace ConditionMonitoringAPI.Features.Sensors.Controllers
{
    public class SensorController : AbstractController<Sensor<ISensorReading>, long, CreateSensor, UpdateSensor>
    {
        public SensorController(ConditionMonitoringDbContext context,
            ILogger<SensorController> logger)
            :base(context, logger)
        {
        }
    }
}
