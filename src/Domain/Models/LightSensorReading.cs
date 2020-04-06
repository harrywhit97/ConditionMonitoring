using Domain.Interfaces;
using System;

namespace Domain.Models
{
    public class LightSensorReading : RawSensorReading, IHaveId<long>
    {
        public long Id { get; set; }
        public int Brightness { get; set; }
    }
}
