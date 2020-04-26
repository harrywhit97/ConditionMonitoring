using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using Domain.Interfaces;
using Domain.Models;

namespace ConditionMonitoringAPI.Features.Sensors.Commands
{
    public class DeleteSensor
    {
        public class DeleteSensorHandler : DeleteEntityHandler<Sensor<ISensorReading>, long>
        {
            public DeleteSensorHandler(ConditionMonitoringDbContext context)
                : base(context)
            {
            }
        }
    }
}
