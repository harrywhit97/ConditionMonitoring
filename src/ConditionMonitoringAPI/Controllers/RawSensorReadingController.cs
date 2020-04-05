using ConditionMonitoringAPI.Models;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Controllers
{
    public class RawSensorReadingController : ControllerBase
    {
        public IActionResult Post(DTORawSensorReading dtoReadings)
        {
            return Ok();
        }

        public IActionResult Post(RawSensorReading reading)
        {
            return Ok();
        }
    }
}
