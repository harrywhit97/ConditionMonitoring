using ConditionMonitoringAPI.Dtos;
using Domain.Interfaces;
using MediatR;

namespace ConditionMonitoringAPI.Features.Readings.Commands
{
    public class CreateReading : IRequest<ISensorReading>
    {
        public RawSensorReadingDto RawSensorReadingDto { get; }

        public CreateReading(RawSensorReadingDto rawSensorReadingDto)
        {
            RawSensorReadingDto = RawSensorReadingDto;
        }
    }
}
