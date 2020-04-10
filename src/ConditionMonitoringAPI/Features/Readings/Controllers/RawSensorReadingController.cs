using ConditionMonitoringAPI.Features.Boards.Queries;
using ConditionMonitoringAPI.Features.Readings.Commands;
using ConditionMonitoringAPI.Features.Readings.Dtos;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace ConditionMonitoringAPI.Features.Readings.Controllers
{
    [Route("api/[controller]")]
    public class RawSensorReadingController : ControllerBase
    {
        readonly IMediator Mediator;
        readonly ILogger Logger;

        public RawSensorReadingController(IMediator mediator, ILogger<RawSensorReadingController> logger)
        {
            Mediator = mediator;
            Logger = logger;
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
        public IActionResult Post([FromBody]RawSensorReadingDto readingDto)
        {
            ISensorReading reading;
            try
            {
                readingDto.Pin ??= GetPinFromHeaders();
                readingDto.IpAddress ??= GetIpFromRequest();

                var board = Mediator.Send(new GetBoardByIp(readingDto.IpAddress)).Result;
                reading = Mediator.Send(new CreateReading(readingDto, board)).Result;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
            if (reading is null)
                return BadRequest();

            return Ok(reading);
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
