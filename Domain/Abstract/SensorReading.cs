using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstract
{
    public abstract class SensorReading<TId, TRawReading> : Entity<TId>
    {
        public TId Id { get; set; }
        public DateTimeOffset ReadingTime { get; set; }
        public TRawReading RawReading { get; set; }
    }
}
