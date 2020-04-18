using ConditionMonitoringAPI.Features.Readings.Dtos;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System.Collections.Generic;

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
}
