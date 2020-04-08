using AutoMapper;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using ConditionMonitoringAPI.Features.Readings.Dtos;
using ConditionMonitoringAPI.Features.SensorsReadings.Validators;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace ConditionMonitoringAPI.Features.Readings
{
    public class GetLightSensorReadingByIdHandler : GetEntityByIdHandler<LightSensorReading, long>
    {
        public GetLightSensorReadingByIdHandler(ConditionMonitoringDbContext context, ILogger logger, IMapper mapper)
            : base(context, logger, mapper)
        {
        }
    }

    public class CreateLightSensorReadingHandler : CreateEntityFromDtoHandler<LightSensorReading, long, LightSensorReadingValidator, LightSensorReadingDto>
    {
        public CreateLightSensorReadingHandler(ConditionMonitoringDbContext context, ILogger logger, LightSensorReadingValidator validator, IMapper mapper)
            : base(context, logger, validator, mapper)
        {
        }
    }

    public class UpdateLightSensorReadingHandler : UpdateEntityFromDtoHandler<LightSensorReading, long, LightSensorReadingValidator, LightSensorReadingDto>
    {
        public UpdateLightSensorReadingHandler(ConditionMonitoringDbContext context, ILogger logger, LightSensorReadingValidator validator, IMapper mapper)
            : base(context, logger, validator, mapper)
        {
        }
    }

    public class DeleteLightSensorReadingByIdHandler : DeleteEntityHandler<LightSensorReading, long>
    {
        public DeleteLightSensorReadingByIdHandler(ConditionMonitoringDbContext context, ILogger logger, IMapper mapper)
            : base(context, logger, mapper)
        {
        }
    }
}
