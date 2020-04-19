using ConditionMonitoringAPI.Features.Boards.Queries;
using ConditionMonitoringAPI.Features.Readings.Commands;
using ConditionMonitoringAPI.Features.Readings.Dtos;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

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
        public IActionResult Post([FromBody]RawSensorReadingBatchDto readingsBatchDto)
        {
            Logger.LogDebug("Recieved post batch request");

            IList<ISensorReading> readings;
            try
            {
                readingsBatchDto.Pin ??= GetPinFromHeaders();
                readingsBatchDto.IpAddress ??= GetIpFromRequest();

                readingsBatchDto.Board = Mediator.Send(new GetBoardByIp(readingsBatchDto.IpAddress)).Result;

                readings = Mediator.Send(new CreateBatchOfReadings(readingsBatchDto)).Result;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "There was an error processing a Post batch request");
                return BadRequest(e.Message);
            }

            return Ok(readings);
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
                Logger.LogError(e, "There was an error processing a Post request");
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
