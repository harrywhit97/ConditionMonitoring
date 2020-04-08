using ConditionMonitoringAPI.Abstract;
using Domain.Models;
using MediatR;

namespace ConditionMonitoringAPI.Features.Readings.Controllers
{
    public class LightSensorReadingController : GenericController<LightSensorReading, long>
    {
        public LightSensorReadingController(ConditionMonitoringDbContext context, IMediator mediator)
            :base(context, mediator)
        {
        }
    }
}
