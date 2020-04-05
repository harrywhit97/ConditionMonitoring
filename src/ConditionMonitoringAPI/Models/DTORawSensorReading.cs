using Domain.Models;
using System.Collections.Generic;

namespace ConditionMonitoringAPI.Models
{
    public class DTORawSensorReading
    {
        public IList<RawSensorReading> Readings { get; set; }
    }
}
