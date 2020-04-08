using Domain.Enums;

namespace ConditionMonitoringAPI.Features.Sensors.Dtos
{
    public class SensorDto
    {
        public string Name { get; set; }
        public long Address { get; set; }
        public long BoardId { get; set; }
        public long Pin { get; set; }
        public SensorType SensorType { get; set; }
    }
}
