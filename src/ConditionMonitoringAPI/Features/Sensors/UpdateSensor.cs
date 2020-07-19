using Domain.Interfaces;
using Domain.Models;
using WebApiUtilities.CrudRequests;

namespace ConditionMonitoringAPI.Features.Sensors
{
    public class UpdateSensor : SensorDto, IUpdateCommand<Sensor<ISensorReading>, long>
    {
    }
}
