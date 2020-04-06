using Domain.Models;
using System.Collections.Generic;

namespace ConditionMonitoringAPI.Dtos
{
    public class RawSensorReadingBatchDto
    {
        public IList<RawSensorReadingDto> Readings { get; set; }
    }
}
