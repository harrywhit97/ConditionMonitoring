using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Board : IHaveId<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public virtual IList<ISensor> Sensors { get; set; }

        public Board()
        {
            Sensors = new List<ISensor>();
        }
    }
}
