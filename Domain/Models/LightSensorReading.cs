using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class LightSensorReading : SensorReading<long, int>
    {
        public int Brightness { get; set; }
    }
}
