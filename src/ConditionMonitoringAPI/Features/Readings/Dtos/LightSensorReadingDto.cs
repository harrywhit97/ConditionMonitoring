using Domain.Interfaces;
using Domain.Models;
using System;
using System.Text.Json.Serialization;

namespace ConditionMonitoringAPI.Features.Readings.Dtos
{
    public class LightSensorReadingDto
    {
        public DateTimeOffset ReadingTime { get; set; }
        public long SensorId { get; set; }
        public decimal RawReading { get; set; }
        public int Brightness { get; set; }
        [JsonIgnore]
        public Sensor<ISensorReading> Sensor { get; set; }
    }
}
