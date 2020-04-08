using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Features.Readings.Dtos;
using Domain.Models;
using MediatR;

namespace ConditionMonitoringAPI.Features.Readings.Controllers
{
    public class LightSensorReadingController : GenericController<LightSensorReading, long, LightSensorReadingDto>
    {
        public LightSensorReadingController(ConditionMonitoringDbContext context, IMediator mediator)
            :base(context, mediator)
        {
        }
    }
}
