using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Validators;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Controllers
{
    public class LightSensorReadingController : GenericController<LightSensorReading, long, LightSensorReadingValidator>
    {
        public LightSensorReadingController(ConditionMonitoringContext context, LightSensorReadingValidator validator)
            :base(context, validator)
        {
        }
    }
}
