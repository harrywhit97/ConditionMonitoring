using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using Domain.Models;

namespace ConditionMonitoringAPI.Features.Readings.Queries
{
    public class GetLightSensorReadingQueries
    {
        public class GetLightSensorReadingHandler : GetEntitiesHandler<LightSensorReading, long>
        {
            public GetLightSensorReadingHandler(ConditionMonitoringDbContext context)
                : base(context)
            {
            }
        }

        public class GetLightSensorReadingByIdHandler : GetEntityByIdHandler<LightSensorReading, long>
        {
            public GetLightSensorReadingByIdHandler(ConditionMonitoringDbContext context)
                : base(context)
            {
            }
        }
    }
}
