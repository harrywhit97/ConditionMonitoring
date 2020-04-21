using Domain.Interfaces;
using ConditionMonitoringAPI.Features.Sensors.Dtos;
using Domain.Models;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using AutoMapper;
using System.Threading.Tasks;
using System.Threading;
using ConditionMonitoringAPI.Exceptions;
using System.Net;

namespace ConditionMonitoringAPI.Features.Sensors
{
    public class SensorHandlers
    {
        public class GetSensorsHandler : GetEntitiesHandler<Sensor<ISensorReading>, long>
        {
            public GetSensorsHandler (ConditionMonitoringDbContext context)
                : base(context)
            {
            }
        }

        public class GetSensorByIdHandler : GetEntityByIdHandler<Sensor<ISensorReading>, long>
        {
            public GetSensorByIdHandler(ConditionMonitoringDbContext context)
                : base(context)
            {
            }
        }

        public class CreateSensorHandler : CreateEntityFromDtoHandler<Sensor<ISensorReading>, long, SensorValidator, SensorDto>
        {
            public CreateSensorHandler(ConditionMonitoringDbContext context, SensorValidator validator, IMapper mapper)
                : base(context, validator, mapper)
            {
            }
        }

        public class UpdateSensorHandler : UpdateEntityFromDtoHandler<Sensor<ISensorReading>, long, SensorValidator, SensorDto>
        {
            public UpdateSensorHandler(ConditionMonitoringDbContext context, SensorValidator validator, IMapper mapper)
                : base(context, validator, mapper)
            {
            }

            public override Task<Sensor<ISensorReading>> Handle(UpdateEntityFromDto<Sensor<ISensorReading>, long, SensorDto> request, CancellationToken cancellationToken)
            {
                request.Dto.Board = Context.Set<Board>().Find(request.Dto.BoardId);

                _ = request.Dto.Board ?? throw new BadRequestException($"Could not find a board with an Id of {request.Dto.BoardId}");

                return base.Handle(request, cancellationToken);
            }
        }

        public class DeleteSensorByIdHandler : DeleteEntityHandler<Sensor<ISensorReading>, long>
        {
            public DeleteSensorByIdHandler(ConditionMonitoringDbContext context)
                : base(context)
            {
            }
        }
    }
}
