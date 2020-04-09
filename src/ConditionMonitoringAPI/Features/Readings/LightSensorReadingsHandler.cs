using AutoMapper;
using ConditionMonitoringAPI.Exceptions;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using ConditionMonitoringAPI.Features.Readings.Dtos;
using ConditionMonitoringAPI.Features.SensorsReadings.Validators;
using Domain.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Readings
{
    public class GetLightSensorReadingByIdHandler : GetEntityByIdHandler<LightSensorReading, long>
    {
        public GetLightSensorReadingByIdHandler(ConditionMonitoringDbContext context, ILogger<GetLightSensorReadingByIdHandler> logger, IMapper mapper)
            : base(context, logger, mapper)
        {
        }
    }

    public class CreateLightSensorReadingHandler : CreateEntityFromDtoHandler<LightSensorReading, long, LightSensorReadingValidator, LightSensorReadingDto>
    {
        public CreateLightSensorReadingHandler(ConditionMonitoringDbContext context, ILogger<CreateLightSensorReadingHandler> logger, LightSensorReadingValidator validator, IMapper mapper)
            : base(context, logger, validator, mapper)
        {
        }
    }

    public class UpdateLightSensorReadingHandler : UpdateEntityFromDtoHandler<LightSensorReading, long, LightSensorReadingValidator, LightSensorReadingDto>
    {
        public UpdateLightSensorReadingHandler(ConditionMonitoringDbContext context, ILogger<UpdateLightSensorReadingHandler> logger, LightSensorReadingValidator validator, IMapper mapper)
            : base(context, logger, validator, mapper)
        {
        }

        public override Task<LightSensorReading> Handle(UpdateEntityFromDto<LightSensorReading, long, LightSensorReadingDto> request, CancellationToken cancellationToken)
        {
            request.Dto.Sensor = Context.Sensors.Find(request.Dto.SensorId);

            _ = request.Dto.Sensor ?? throw new RestException(HttpStatusCode.BadRequest, $"Could not find a sensor with an Id of {request.Dto.SensorId}");

            return base.Handle(request, cancellationToken);
        }
    }

    public class DeleteLightSensorReadingByIdHandler : DeleteEntityHandler<LightSensorReading, long>
    {
        public DeleteLightSensorReadingByIdHandler(ConditionMonitoringDbContext context, ILogger<DeleteLightSensorReadingByIdHandler> logger, IMapper mapper)
            : base(context, logger, mapper)
        {
        }
    }
}
