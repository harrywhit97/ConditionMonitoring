using Domain.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Board : IHasId<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public virtual ICollection<Sensor<ISensorReading>> Sensors { get; }

        public Board()
        {
            Sensors = new List<Sensor<ISensorReading>>();
        }
    }
}
