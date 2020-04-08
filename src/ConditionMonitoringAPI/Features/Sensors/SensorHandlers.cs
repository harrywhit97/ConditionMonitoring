using Domain.Interfaces;
using ConditionMonitoringAPI.Features.Sensors.Dtos;
using ConditionMonitoringAPI.Features.Crosscutting;
using Domain.Models;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using Microsoft.Extensions.Logging;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using AutoMapper;

namespace ConditionMonitoringAPI.Features.Sensors
{
    public class SensorHandlers : BaseHandlers<Sensor<ISensorReading>, long, SensorValidator, SensorDto>
    {
        public class GetSensorByIdHandler : GetEntityByIdHandler<Sensor<ISensorReading>, long>
        {
            public GetSensorByIdHandler(ConditionMonitoringDbContext context, ILogger logger, IMapper mapper)
                : base(context, logger, mapper)
            {
            }
        }

        public class CreateSensorHandler : CreateEntityFromDtoHandler<Sensor<ISensorReading>, long, SensorValidator, SensorDto>
        {
            public CreateSensorHandler(ConditionMonitoringDbContext context, ILogger logger, SensorValidator validator, IMapper mapper)
                : base(context, logger, validator, mapper)
            {
            }
        }

        public class UpdateSensorHandler : UpdateEntityFromDtoHandler<Sensor<ISensorReading>, long, SensorValidator, SensorDto>
        {
            public UpdateSensorHandler(ConditionMonitoringDbContext context, ILogger logger, SensorValidator validator, IMapper mapper)
                : base(context, logger, validator, mapper)
            {
            }
        }

        public class DeleteSensorByIdHandler : DeleteEntityHandler<Sensor<ISensorReading>, long>
        {
            public DeleteSensorByIdHandler(ConditionMonitoringDbContext context, ILogger logger, IMapper mapper)
                : base(context, logger, mapper)
            {
            }
        }
    }
}
