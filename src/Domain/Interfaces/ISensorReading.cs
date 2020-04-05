using System;

namespace Domain.Interfaces
{
    public interface ISensorReading<TRawReading> : IHaveId<long>
    {
        public long Id { get; set; }
        public DateTimeOffset ReadingTime { get; set; }
        public TRawReading RawReading { get; set; }
    }
}
