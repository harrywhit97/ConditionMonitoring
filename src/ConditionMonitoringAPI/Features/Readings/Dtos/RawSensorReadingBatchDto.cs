using Domain.Models;
using System.Collections.Generic;

namespace ConditionMonitoringAPI.Features.Readings.Dtos
{
    public class RawSensorReadingBatchDto
    {
        public IList<RawSensorReadingDto> Readings { get; set; }
    }
}
