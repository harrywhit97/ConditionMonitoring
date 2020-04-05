using Domain.Interfaces;
using System;

namespace Domain.Models
{
    public class RawSensorReading
    {
        public DateTimeOffset? TimeStamp { get; set; }
        public decimal? Reading { get; set; }
        public long? Address { get; set; }
        public int? Pin { get; set; }
        public Board Board { get; set; }
        public ISensor Sensor { get; set; }
    }
}
