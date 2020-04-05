using Domain.Interfaces;
using System;

namespace Domain.Models
{
    public class LightSensorReading : ISensorReading<int>
    {
        public long Id { get; set; }
        public int RawReading { get; set; }
        public DateTimeOffset ReadingTime { get; set; }
        public int Brightness { get; set; }        
    }
}
