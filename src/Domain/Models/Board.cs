using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Board : IHaveId<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public ICollection<LightSensor> Sensors { get; set; }
    }
}
