using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using System.Text.Json.Serialization;
using WebApiUtilities.Abstract;

namespace ConditionMonitoringAPI.Features.Sensors
{
    public abstract class SensorDto : Dto<Sensor<ISensorReading>, long>
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
