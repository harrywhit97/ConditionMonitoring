using Domain.Interfaces;
using System;

namespace Domain.Models
{
    public class LightSensorReading : ISensorReading
    {
        public long Id { get; set; }        
        public DateTimeOffset ReadingTime { get; set; }
        public Sensor<ISensorReading> Sensor { get; set; }
        public decimal RawReading { get; set; }
        public int Brightness { get; set; }

    }
}
