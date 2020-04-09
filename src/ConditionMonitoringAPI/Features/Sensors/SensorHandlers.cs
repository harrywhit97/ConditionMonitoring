using Domain.Interfaces;
using ConditionMonitoringAPI.Features.Sensors.Dtos;
using ConditionMonitoringAPI.Features.Crosscutting;
using Domain.Models;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using Microsoft.Extensions.Logging;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using AutoMapper;
using System.Threading.Tasks;
using System.Threading;
using ConditionMonitoringAPI.Exceptions;
using System.Net;

namespace ConditionMonitoringAPI.Features.Sensors
{
    public class SensorHandlers : BaseHandlers<Sensor<ISensorReading>, long, SensorValidator, SensorDto>
    {
        public class GetSensorByIdHandler : GetEntityByIdHandler<Sensor<ISensorReading>, long>
        {
            public GetSensorByIdHandler(ConditionMonitoringDbContext context, ILogger<GetSensorByIdHandler> logger, IMapper mapper)
                : base(context, logger, mapper)
            {
            }
        }

        public class CreateSensorHandler : CreateEntityFromDtoHandler<Sensor<ISensorReading>, long, SensorValidator, SensorDto>
        {
            public CreateSensorHandler(ConditionMonitoringDbContext context, ILogger<CreateSensorHandler> logger, SensorValidator validator, IMapper mapper)
                : base(context, logger, validator, mapper)
            {
            }
        }

        public class UpdateSensorHandler : UpdateEntityFromDtoHandler<Sensor<ISensorReading>, long, SensorValidator, SensorDto>
        {
            public UpdateSensorHandler(ConditionMonitoringDbContext context, ILogger<UpdateSensorHandler> logger, SensorValidator validator, IMapper mapper)
                : base(context, logger, validator, mapper)
            {
            }

            public override Task<Sensor<ISensorReading>> Handle(UpdateEntityFromDto<Sensor<ISensorReading>, long, SensorDto> request, CancellationToken cancellationToken)
            {
                request.Dto.Board = Context.Boards.Find(request.Dto.BoardId);

                _ = request.Dto.Board ?? throw new RestException(HttpStatusCode.BadRequest, $"Could not find a board with an Id of {request.Dto.BoardId}");

                return base.Handle(request, cancellationToken);
            }
        }

        public class DeleteSensorByIdHandler : DeleteEntityHandler<Sensor<ISensorReading>, long>
        {
            public DeleteSensorByIdHandler(ConditionMonitoringDbContext context, ILogger<DeleteSensorByIdHandler> logger, IMapper mapper)
                : base(context, logger, mapper)
            {
            }
        }
    }
}
