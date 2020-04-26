using AutoMapper;
using ConditionMonitoringAPI.Features.Common.Commands;
using ConditionMonitoringAPI.Features.Sensors.Dtos;
using ConditionMonitoringAPI.Mapping;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace ConditionMonitoringAPI.Features.Sensors.Commands
{
    public class UpdateSensor : SensorDto, IRequest<Sensor<ISensorReading>>, IHasId<long>, IMapFrom<Sensor<ISensorReading>>
    {
        public long Id { get; set; }
    }

    public class UpdateSensorHandler : UpdateEntityFromRequestHandler<Sensor<ISensorReading>, long, UpdateSensor>
    {
        public UpdateSensorHandler(ConditionMonitoringDbContext context, IMapper mapper)
            :base(context, mapper)
        {
        }
    }
}
