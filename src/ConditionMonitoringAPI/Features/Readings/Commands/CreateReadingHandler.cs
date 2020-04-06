using AutoMapper;
using ConditionMonitoringAPI.Features.Boards.Queries;
using ConditionMonitoringCore;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Readings.Commands
{
    public class CreateReadingHandler : IRequestHandler<CreateReading, ISensorReading>
    {

        readonly ILogger Logger;
        readonly Mapper Mapper;
        readonly Mediator Mediator;

        public CreateReadingHandler(ILogger logger, Mapper mapper, Mediator mediator)
        {
            Logger = logger;
            Mapper = mapper;
            Mediator = mediator;
        }

        public async Task<ISensorReading> Handle(CreateReading request, CancellationToken cancellationToken)
        {
            Logger.LogInformation($"{nameof(CreateReadingHandler)} recieved a request");

            var dto = request.RawSensorReadingDto;

            var rawReading = Mapper.Map<RawSensorReading>(dto);

            var board = await Mediator.Send(new GetBoardByIp(dto.IpAddress));

            var sensor = board.Sensors.Where(x => x.Pin == dto.Pin).FirstOrDefault();

            if (sensor is null)
                throw new Exception($"A sensor that has a pin of {dto.Pin} was not found.");

            rawReading.Sensor = sensor;

            var reading = SensorReadingFactory.GetSensorReading(rawReading);

            return reading;
        }
    }
}
