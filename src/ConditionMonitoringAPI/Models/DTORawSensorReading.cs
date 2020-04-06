using System;

namespace Domain.Models
{
    public class DTORawSensorReading
    {
        public DateTimeOffset? TimeStamp { get; set; }
        public decimal Reading { get; set; }
        public long IpAddress { get; set; }
        public long Pin { get; set; }
        public long CommsType { get; set; } // Enum?
    }
}
