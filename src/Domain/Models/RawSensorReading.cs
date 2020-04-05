using Domain.Interfaces;
using System;

namespace Domain.Models
{
    public class RawSensorReading
    {
        public DateTimeOffset? TimeStamp { get; set; }
        public decimal Reading { get; set; }
        public long IpAddress { get; set; }
        public long Pin { get; set; }
        public long CommsType { get; set; } // Enum?
    }
}
