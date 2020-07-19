using Domain.Models;
using WebApiUtilities.CrudRequests;

namespace ConditionMonitoringAPI.Features.Readings
{
    public class CreateLightSensorReading : LightSensorReadingDto, ICreateCommand<LightSensorReading, long>
    {
    }
}
