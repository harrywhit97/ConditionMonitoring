using AutoMapper;
using ConditionMonitoringAPI.Exceptions;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using ConditionMonitoringAPI.Features.Readings.Dtos;
using ConditionMonitoringAPI.Features.SensorsReadings.Validators;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Readings
{
    public class GetLightSensorReadingsHandler : GetEntitiesHandler<LightSensorReading, long>
    {
        public GetLightSensorReadingsHandler(ConditionMonitoringDbContext context)
            : base(context)
        {
        }
    }

    public class GetLightSensorReadingByIdHandler : GetEntityByIdHandler<LightSensorReading, long>
    {
        public GetLightSensorReadingByIdHandler(ConditionMonitoringDbContext context)
            : base(context)
        {
        }
    }

    public class CreateLightSensorReadingHandler : CreateEntityFromDtoHandler<LightSensorReading, long, LightSensorReadingValidator, LightSensorReadingDto>
    {
        public CreateLightSensorReadingHandler(ConditionMonitoringDbContext context, LightSensorReadingValidator validator, IMapper mapper)
            : base(context, validator, mapper)
        {
        }
    }

    public class UpdateLightSensorReadingHandler : UpdateEntityFromDtoHandler<LightSensorReading, long, LightSensorReadingValidator, LightSensorReadingDto>
    {
        public UpdateLightSensorReadingHandler(ConditionMonitoringDbContext context, ILogger<UpdateLightSensorReadingHandler> logger, LightSensorReadingValidator validator, IMapper mapper)
            : base(context, validator, mapper)
        {
        }

        public override Task<LightSensorReading> Handle(UpdateEntityFromDto<LightSensorReading, long, LightSensorReadingDto> request, CancellationToken cancellationToken)
        {
            request.Dto.Sensor = Context.Set<Sensor<ISensorReading>>().Find(request.Dto.SensorId);

            _ = request.Dto.Sensor ?? throw new BadRequestException($"Could not find a sensor with an Id of {request.Dto.SensorId}");

            return base.Handle(request, cancellationToken);
        }
    }

    public class DeleteLightSensorReadingByIdHandler : DeleteEntityHandler<LightSensorReading, long>
    {
        public DeleteLightSensorReadingByIdHandler(ConditionMonitoringDbContext context)
            : base(context)
        {
        }
    }
}
