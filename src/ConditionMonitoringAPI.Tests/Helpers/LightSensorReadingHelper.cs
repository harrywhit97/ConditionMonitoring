using Domain.Models;
using System;

namespace ConditionMonitoringAPI.Tests.Helpers
{
    public class LightSensorReadingHelper
    {
        public long Id { get; set; }
        public int RawReading { get; set; }
        public DateTimeOffset ReadingTime { get; set; }
        public int Brightness { get; set; }

        public LightSensorReadingHelper(
            DateTimeOffset readingTime = default,
            long id = 0,
            int rawReading = 0,
            int brightness = 0)
        {
            Id = id;
            RawReading = rawReading;
            ReadingTime = readingTime;
            Brightness = brightness;
        }

        public LightSensorReading GetNewReading()
        {
            return new LightSensorReading()
            { 
                Id = Id,
                RawReading = RawReading,
                ReadingTime = ReadingTime,
                Brightness = Brightness
            };
        }
    }
}
