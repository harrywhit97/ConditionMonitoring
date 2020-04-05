using Domain.Interfaces;

namespace Domain.Models
{
    public class LightSensor : ISensor
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Address { get; set; }
        public virtual Board Board { get; set; }
        public long Pin { get; set; }
        public long CommsType { get; set; }
    }
}
