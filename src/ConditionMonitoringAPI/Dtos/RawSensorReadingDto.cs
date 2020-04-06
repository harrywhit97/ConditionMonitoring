using System;

namespace ConditionMonitoringAPI.Dtos
{
    public class RawSensorReadingDto
    {
        public DateTimeOffset ReadingTime { get; set; }
        public decimal Reading { get; set; }
        public string IpAddress { get; set; }
        public long Pin { get; set; }
    }
}
