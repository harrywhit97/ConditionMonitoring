using Domain.Interfaces;
using System;

namespace Domain.Models
{
    public class RawSensorReading : ISensorReading
    {
        public virtual LightSensor Sensor { get; set; }
        public DateTimeOffset ReadingTime { get; set; }
        public decimal RawReading { get; set; }
    }
}
