using Domain.Interfaces;
using Domain.Models;
using WebApiUtilities.CrudRequests;

namespace ConditionMonitoringAPI.Features.Sensors
{
    public class CreateSensor : SensorDto, ICreateCommand<Sensor<ISensorReading>, long>
    {
    }
}
