using Domain.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ConditionMonitoringAPI.Features.Readings.Dtos
{
    public class RawSensorReadingBatchDto
    {
        public string? IpAddress { get; set; }
        public long? Pin { get; set; }
        public IList<RawSensorReadingDto> Readings { get; set; }
        [JsonIgnore]
        public Board Board { get; set; }
    }
}
