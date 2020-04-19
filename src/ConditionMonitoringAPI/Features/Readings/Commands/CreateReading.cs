using AutoMapper;
using ConditionMonitoringAPI.Features.Readings.Dtos;
using ConditionMonitoringCore;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Readings.Commands
{
    public class CreateReading : IRequest<ISensorReading>
    {
        public RawSensorReadingDto RawSensorReadingDto { get; }
        public Board Board { get; set; }

        public CreateReading(RawSensorReadingDto rawSensorReadingDto, Board board)
        {
            RawSensorReadingDto = rawSensorReadingDto;
            Board = board;
        }
    }

    public class CreateReadingHandler : IRequestHandler<CreateReading, ISensorReading>
    {
        readonly ConditionMonitoringDbContext Context;
        readonly IMapper Mapper;

        public CreateReadingHandler(ConditionMonitoringDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public Task<ISensorReading> Handle(CreateReading request, CancellationToken cancellationToken)
        {
            var pin = request.RawSensorReadingDto.Pin;

            var sensor = request.Board.Sensors
                .Where(x => x.Pin == pin)
                .FirstOrDefault();

            _ = sensor ?? throw new Exception($"A sensor that has a pin of {pin} was not found.");

            var reading = Mapper.Map<ISensorReading>(request.RawSensorReadingDto);

            reading.Sensor = sensor;

            reading.Calculate();

            return (Task<ISensorReading>)reading;
        }
    }
}
