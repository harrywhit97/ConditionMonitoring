using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class LightSensor : ISensor
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
