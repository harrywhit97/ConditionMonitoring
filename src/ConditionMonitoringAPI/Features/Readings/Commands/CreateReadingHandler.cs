using AutoMapper;
using ConditionMonitoringCore;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Readings.Commands
{
    public class CreateReadingHandler : IRequestHandler<CreateReading, ISensorReading>
    {

        readonly ILogger Logger;
        readonly IMapper Mapper;

        public CreateReadingHandler(ILogger<CreateReadingHandler> logger, IMapper mapper)
        {
            Logger = logger;
            Mapper = mapper;
        }

        public Task<ISensorReading> Handle(CreateReading request, CancellationToken cancellationToken)
        {
            Logger.LogInformation($"{nameof(CreateReadingHandler)} recieved a request");

            var rawReading = Mapper.Map<RawSensorReading>(request.RawSensorReadingDto);

            var pin = request.RawSensorReadingDto.Pin;

            var sensor = request.Board.Sensors.Where(x => x.Pin == pin).FirstOrDefault();

            _ = sensor ?? throw new Exception($"A sensor that has a pin of {pin} was not found.");

            rawReading.Sensor = sensor;

            var reading = SensorReadingFactory.GetSensorReading(rawReading);

            return (Task<ISensorReading>)reading;
        }
    }
}
