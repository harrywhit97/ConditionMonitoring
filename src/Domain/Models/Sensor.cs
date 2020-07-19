using Domain.Enums;
using Domain.Interfaces;
using System.Collections.Generic;
using WebApiUtilities.Abstract;

namespace Domain.Models
{
    public class Sensor<TReading> : Entity<long>
        where TReading : ISensorReading
    {
        public string Name { get; set; }
        public long Address { get; set; }
        public long BoardId { get; set; }
        public long Pin { get; set; }
        public SensorType SensorType { get; set; }

        public virtual ICollection<TReading> Readings { get; }
        public virtual Board Board { get; set; }

        public Sensor()
        {
            Readings = new List<TReading>();
        }
    }
}
