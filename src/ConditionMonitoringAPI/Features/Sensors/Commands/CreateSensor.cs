using AutoMapper;
using ConditionMonitoringAPI.Features.Common.Commands;
using ConditionMonitoringAPI.Features.Sensors.Dtos;
using ConditionMonitoringAPI.Mapping;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace ConditionMonitoringAPI.Features.Sensors.Commands
{
    public class CreateSensor : SensorDto, IRequest<Sensor<ISensorReading>>, IMapFrom<Sensor<ISensorReading>>
    {
    }

    public class CreateSensorHandler : CreateEntityFromRequestHandler<Sensor<ISensorReading>, long, CreateSensor>
    {
        public CreateSensorHandler(ConditionMonitoringDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }
    }
}
