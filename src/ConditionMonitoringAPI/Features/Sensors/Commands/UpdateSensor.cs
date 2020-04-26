using AutoMapper;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Sensors.Dtos;
using Domain.Interfaces;
using Domain.Models;

namespace ConditionMonitoringAPI.Features.Sensors.Commands
{
    public class UpdateSensor : SensorDto, IUpdateEntityFromRequest<Sensor<ISensorReading>, long>
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
