using Domain.Enums;
using Domain.Models;
using System.Text.Json.Serialization;

namespace ConditionMonitoringAPI.Features.Sensors.Dtos
{
    public class SensorDto
    {
        public string Name { get; set; }
        public long Address { get; set; }
        public long BoardId { get; set; }
        public long Pin { get; set; }
        public SensorType SensorType { get; set; }
        [JsonIgnore]
        public Board Board { get; set; }
    }
}
