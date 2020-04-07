using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Validators;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Controllers
{
    public class LightSensorReadingController : WriteOnlyController<LightSensorReading, long, LightSensorReadingValidator>
    {
        public LightSensorReadingController(ConditionMonitoringDbContext context, LightSensorReadingValidator validator)
            :base(context, validator)
        {
        }
    }
}
