using ConditionMonitoringAPI.Models;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Controllers
{
    [Route("api/[controller]")]
    public class RawSensorReadingController : ControllerBase
    {
        [HttpPost]
        [Route("batch")]
        public IActionResult Post([FromBody]DTORawSensorReading dtoReadings)
        {
            int pin;
            try
            {
                pin = GetPinFromHeaders();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            

            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody]RawSensorReading reading)
        {
            return Ok();
        }

        int GetPinFromHeaders()
        {
            if (!Request.Headers.TryGetValue("pin", out var pinValue))
                throw new Exception("Header 'pin' was not found");

            if (!int.TryParse(pinValue, out var pin))
                throw new Exception("Header 'pin' was not an integer");

            return pin;
        }

        IList<RawSensorReading> SetValues(DTORawSensorReading dTORawSensorReading, string ip, int pin, string address)
        {
            var readings = dTORawSensorReading.Readings;
            for (int i = 0; i < readings.Count; i++)
            {
            }

            return null;
        }
    }
}
