using AutoMapper;
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
        readonly IMapper Mapper;

        public CreateReadingHandler(ILogger<CreateReadingHandler> logger, IMapper mapper)
        {
            Logger = logger;
            Mapper = mapper;
        }

        public Task<ISensorReading> Handle(CreateReading request, CancellationToken cancellationToken)
        {
            Logger.LogInformation($"{nameof(CreateReadingHandler)} recieved a request");

            var pin = request.RawSensorReadingDto.Pin;

            var sensor = request.Board.Sensors.Where(x => x.Pin == pin).FirstOrDefault();

            _ = sensor ?? throw new Exception($"A sensor that has a pin of {pin} was not found.");

            var reading = Mapper.Map<ISensorReading>(request.RawSensorReadingDto);

            reading.Sensor = sensor;

            reading.Calculate();

            return (Task<ISensorReading>)reading;
        }
    }
}
