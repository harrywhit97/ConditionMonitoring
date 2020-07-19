using Domain.Interfaces;
using System.Collections.Generic;
using WebApiUtilities.Abstract;

namespace Domain.Models
{
    public class Board : Entity<long>
    {
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public virtual ICollection<Sensor<ISensorReading>> Sensors { get; }

        public Board()
        {
            Sensors = new List<Sensor<ISensorReading>>();
        }
    }
}
