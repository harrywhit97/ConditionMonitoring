using Domain.Enums;
using Domain.Models;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface ISensor<TReading> : IHaveId<long> where TReading : class, ISensorReading
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Pin { get; set; }
        public SensorType SensorType { get; set; }
        public Board Board { get; set; }
        public ICollection<TReading> Readings { get; set; }
    }
}
