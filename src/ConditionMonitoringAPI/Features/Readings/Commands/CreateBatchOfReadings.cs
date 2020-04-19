using AutoMapper;
using ConditionMonitoringAPI.Features.Readings.Dtos;
using ConditionMonitoringCore;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Readings.Commands
{
    public class CreateBatchOfReadings : IRequest<IList<ISensorReading>>
    {
        public RawSensorReadingBatchDto RawSensorReadingDto { get; }

        public CreateBatchOfReadings(RawSensorReadingBatchDto rawSensorReadingDto)
        {
            RawSensorReadingDto = rawSensorReadingDto;
        }
    }

    public class CreateBatchOfReadingsHandler : IRequestHandler<CreateBatchOfReadings, IList<ISensorReading>>
    {
        readonly IMapper Mapper;
        readonly ConditionMonitoringDbContext Context;

        public CreateBatchOfReadingsHandler(ConditionMonitoringDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        async Task<IList<ISensorReading>> IRequestHandler<CreateBatchOfReadings, IList<ISensorReading>>.Handle(CreateBatchOfReadings request, CancellationToken cancellationToken)
        {
            var pin = request.RawSensorReadingDto.Pin;

            var sensor = request.RawSensorReadingDto.Board.Sensors
                .Where(x => x.Pin == pin)
                .FirstOrDefault();

            _ = sensor ?? throw new Exception($"A sensor that has a pin of {pin} was not found.");

            var readings = new List<ISensorReading>();

            foreach (var readingDto in request.RawSensorReadingDto.Readings)
            {
                var reading = Mapper.Map<ISensorReading>(request.RawSensorReadingDto);
                reading.Sensor = sensor;
                reading.Calculate();
                //TODO make this generic
                var n = Context.LightSensorReadings.Add(reading as LightSensorReading);
                readings.Add(reading);
            }

            await Context.SaveChangesAsync();

            return readings;
        }
    }
}
