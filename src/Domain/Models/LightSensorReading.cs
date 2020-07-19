using Domain.Interfaces;
using System;
using WebApiUtilities.Abstract;

namespace Domain.Models
{
    public class LightSensorReading : Entity<long>, ISensorReading
    {
        public DateTimeOffset ReadingTime { get; set; }
        public Sensor<ISensorReading> Sensor { get; set; }
        public decimal RawReading { get; set; }
        public int Brightness { get; set; }

    }
}
