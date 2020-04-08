using ConditionMonitoringAPI.Features.Readings.Dtos;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

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
}
