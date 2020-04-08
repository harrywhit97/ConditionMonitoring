using Domain.Models;
using System;

namespace Domain.Interfaces
{
    public interface ISensorReading : IHasId<long>
    {
        public DateTimeOffset ReadingTime { get; set; }
        public Sensor<ISensorReading> Sensor { get; set; }
        public decimal RawReading { get; set; }
    }
}
