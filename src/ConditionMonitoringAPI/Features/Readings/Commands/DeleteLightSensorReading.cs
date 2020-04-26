using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using Domain.Interfaces;
using Domain.Models;

namespace ConditionMonitoringAPI.Features.Readings.Commands
{
    public class DeleteLightSensorReading
    {
        public class DeleteLightSensorReadingHandler : DeleteEntityHandler<LightSensorReading, long>
        {
            public DeleteLightSensorReadingHandler(ConditionMonitoringDbContext context)
                : base(context)
            {
            }
        }
    }
}
