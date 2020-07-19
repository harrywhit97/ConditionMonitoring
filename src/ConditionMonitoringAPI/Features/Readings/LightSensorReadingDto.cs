using Domain.Interfaces;
using Domain.Models;
using System;
using System.Text.Json.Serialization;
using WebApiUtilities.Abstract;

namespace ConditionMonitoringAPI.Features.Readings
{
    public class LightSensorReadingDto : Dto<LightSensorReading, long>
    {
        public DateTimeOffset ReadingTime { get; set; }
        public long SensorId { get; set; }
        public decimal RawReading { get; set; }
        public int Brightness { get; set; }
        [JsonIgnore]
        public Sensor<ISensorReading> Sensor { get; set; }
    }
}
