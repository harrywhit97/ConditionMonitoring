using ConditionMonitoringAPI.Features.Common.Queries;
using Domain.Interfaces;
using Domain.Models;

namespace ConditionMonitoringAPI.Features.Sensors.Queries
{
    public class GetSensorQueries
    {
        public class GetSensorsHandler : GetEntitiesHandler<Sensor<ISensorReading>, long>
        {
            public GetSensorsHandler(ConditionMonitoringDbContext context)
                : base(context)
            {
            }
        }

        public class GetSensorByIdHandler : GetEntityByIdHandler<Sensor<ISensorReading>, long>
        {
            public GetSensorByIdHandler(ConditionMonitoringDbContext context)
                : base(context)
            {
            }
        }
    }
}
