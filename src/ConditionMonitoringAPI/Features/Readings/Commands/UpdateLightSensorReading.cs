using AutoMapper;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Readings.Dtos;
using Domain.Models;

namespace ConditionMonitoringAPI.Features.Readings.Commands
{
    public class UpdateLightSensorReading : LightSensorReadingDto, IUpdateEntityFromRequest<LightSensorReading, long>
    {
        public long Id { get; set; }
    }

    public class UpdateLightSensorReadingHandler : UpdateEntityFromRequestHandler<LightSensorReading, long, UpdateLightSensorReading>
    {
        public UpdateLightSensorReadingHandler(ConditionMonitoringDbContext context, IMapper mapper)
            :base(context, mapper)
        {
        }
    }
}
