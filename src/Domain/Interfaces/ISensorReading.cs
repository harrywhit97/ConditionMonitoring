using Domain.Models;
using System;

namespace Domain.Interfaces
{
    public interface ISensorReading
    {
        public DateTimeOffset ReadingTime { get; set; }
        public LightSensor Sensor { get; set; }
        public decimal RawReading { get; set; }
    }
}
