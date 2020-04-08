using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Features.Sensors.Dtos;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace ConditionMonitoringAPI.Features.Sensors.Controllers
{
    public class SensorController : GenericController<Sensor<ISensorReading>, long, SensorDto >
    {
        public SensorController(ConditionMonitoringDbContext context, IMediator mediator)
            :base(context, mediator)
        {
        }
    }
}
