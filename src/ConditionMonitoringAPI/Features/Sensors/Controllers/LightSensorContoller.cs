using ConditionMonitoringAPI.Abstract;
using Domain.Models;
using MediatR;

namespace ConditionMonitoringAPI.Features.Sensors.Controllers
{
    public class LightSensorContoller : GenericController<LightSensor, long>
    {
        public LightSensorContoller(ConditionMonitoringDbContext context, IMediator mediator)
            :base(context, mediator)
        {
        }
    }
}
