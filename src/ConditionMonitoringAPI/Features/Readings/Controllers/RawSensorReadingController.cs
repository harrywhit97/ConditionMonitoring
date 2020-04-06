using ConditionMonitoringAPI.Dtos;
using ConditionMonitoringAPI.Features.Readings.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ConditionMonitoringAPI.Features.Readings.Controllers
{
    [Route("api/[controller]")]
    public class RawSensorReadingController : ControllerBase
    {
        IMediator Mediator;

        public RawSensorReadingController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpPost]
        [Route("batch")]
        public IActionResult Post([FromBody]RawSensorReadingBatchDto readingsBatch)
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

            var ip = GetIpFromRequest();
            

            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody]RawSensorReadingDto reading)
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

            reading.Pin = pin;
            reading.IpAddress = GetIpFromRequest();

            var result = Mediator.Send(new CreateReading(reading)).Result;

            if (result is null)
                return BadRequest();
            return Ok(result);
        }

        int GetPinFromHeaders()
        {
            if (!Request.Headers.TryGetValue("pin", out var pinValue))
                throw new Exception("Header 'pin' was not found");

            if (!int.TryParse(pinValue, out var pin))
                throw new Exception("Header 'pin' was not an integer");

            return pin;
        }

        string GetIpFromRequest() => Request.HttpContext.Connection.RemoteIpAddress.ToString();
    }
}
