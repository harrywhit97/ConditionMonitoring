using Domain.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Board : IHaveId<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        [JsonIgnore]
        public ICollection<LightSensor> Sensors { get; set; }
    }
}
