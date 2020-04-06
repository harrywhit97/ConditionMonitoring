using Domain.Models;
using System.Collections.Generic;

namespace ConditionMonitoringAPI.Models
{
    public class DTORawSensorReadingBatch
    {
        public IList<RawSensorReading> Readings { get; set; }
    }
}
