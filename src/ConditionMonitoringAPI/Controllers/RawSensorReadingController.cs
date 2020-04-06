using ConditionMonitoringAPI.Models;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ConditionMonitoringAPI.Controllers
{
    [Route("api/[controller]")]
    public class RawSensorReadingController : ControllerBase
    {
        [HttpPost]
        [Route("batch")]
        public IActionResult Post([FromBody]DTORawSensorReadingBatch dtoReadingsBatch)
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
    }
}
