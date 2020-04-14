using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Features.Readings.Dtos;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConditionMonitoringAPI.Features.Readings.Controllers
{
    public class LightSensorReadingController : AbstractController<LightSensorReading, long, LightSensorReadingDto>
    {
        public LightSensorReadingController(ConditionMonitoringDbContext context, 
            IMediator mediator,
            ILogger<LightSensorReadingController> logger)
            :base(context, mediator, logger)
        {
        }
    }
}
