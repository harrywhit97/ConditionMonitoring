using AutoMapper;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Sensors.Dtos;
using ConditionMonitoringAPI.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace ConditionMonitoringAPI.Features.Sensors.Commands
{
    public class CreateSensor : SensorDto, IRequest<Sensor<ISensorReading>>, IMapToo<Sensor<ISensorReading>>
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
