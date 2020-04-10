using System;

namespace ConditionMonitoringAPI.Features.Readings.Dtos
{
    public class RawSensorReadingDto
    {
        public DateTimeOffset? ReadingTime { get; set; }
        public decimal? ReadingRawReading { get; set; }
        public string? IpAddress { get; set; }
        public long? Pin { get; set; }
    }
}
