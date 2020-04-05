using Domain.Interfaces;
using System;

namespace Domain.Models
{
    public class LightSensorReading : ISensorReading<int>, IHaveId<long>
    {
        public long Id { get; set; }
        public int RawReading { get; set; }
        public int Brightness { get; set; }
        public DateTimeOffset ReadingTime { get; set; }
    }
}
