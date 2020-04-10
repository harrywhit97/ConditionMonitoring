using AutoMapper;
using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringCore;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Readings.Commands
{
    public class CreateReadingHandler : AbstractRequestHandler<ISensorReading, long, CreateReading>
    {

        public CreateReadingHandler(ConditionMonitoringDbContext context, ILogger<CreateReadingHandler> logger, IMapper mapper)
            :base(context, logger, mapper)
        {
        }

        public override Task<ISensorReading> Handle(CreateReading request, CancellationToken cancellationToken)
        {
            Logger.LogInformation($"Recieved a request");

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
