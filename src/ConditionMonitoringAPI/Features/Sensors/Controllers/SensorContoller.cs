using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Features.Sensors.Dtos;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConditionMonitoringAPI.Features.Sensors.Controllers
{
    public class SensorController : AbstractController<Sensor<ISensorReading>, long, SensorDto >
    {
        public SensorController(ConditionMonitoringDbContext context, 
            IMediator mediator, 
            ILogger<SensorController> logger)
            :base(context, mediator, logger)
        {
        }
    }
}
