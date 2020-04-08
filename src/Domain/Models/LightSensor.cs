using Domain.Enums;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class LightSensor : ISensor<LightSensorReading>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Address { get; set; }
        public Board Board { get; set; }
        public long Pin { get; set; }
        public SensorType SensorType { get; set; }
        [JsonIgnore]
        public ICollection<LightSensorReading> Readings { get; set; }
    }
}
