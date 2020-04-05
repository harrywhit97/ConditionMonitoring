using System;

namespace Domain.Interfaces
{
    public interface ISensorReading<TRawReading>
    {
        public DateTimeOffset ReadingTime { get; set; }
        public TRawReading RawReading { get; set; }
    }
}
