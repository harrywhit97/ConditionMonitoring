using Domain.Enums;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Sensor<TReading> : IHasId<long>
        where TReading : ISensorReading
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Address { get; set; }
        public virtual Board Board { get; set; }
        public long Pin { get; set; }
        public SensorType SensorType { get; set; }
       // [JsonIgnore]
        public virtual ICollection<TReading> Readings { get; }

        public Sensor()
        {
            Readings = new List<TReading>();
        }
    }
}
