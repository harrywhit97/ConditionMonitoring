using AutoMapper;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Readings.Dtos;
using ConditionMonitoringAPI.Mapping;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace ConditionMonitoringAPI.Features.Readings.Commands
{
    public class UpdateLightSensorReading : LightSensorReadingDto, IRequest<LightSensorReading>, IHasId<long>, IMapFrom<LightSensorReading>
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
