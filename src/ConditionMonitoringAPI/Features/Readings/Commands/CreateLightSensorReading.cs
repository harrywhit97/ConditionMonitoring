using AutoMapper;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Readings.Dtos;
using ConditionMonitoringAPI.Mapping;
using Domain.Models;
using MediatR;

namespace ConditionMonitoringAPI.Features.Readings.Commands
{
    public class CreateLightSensorReading : LightSensorReadingDto, IRequest<LightSensorReading>, IMapFrom<LightSensorReading>
    {
    }

    public class CreateLightSensorReadingHandler : CreateEntityFromRequestHandler<LightSensorReading, long, CreateLightSensorReading>
    {
        public CreateLightSensorReadingHandler(ConditionMonitoringDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }
    }
}
