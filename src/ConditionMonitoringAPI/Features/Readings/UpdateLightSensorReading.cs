using Domain.Models;
using WebApiUtilities.CrudRequests;

namespace ConditionMonitoringAPI.Features.Readings
{
    public class UpdateLightSensorReading : LightSensorReadingDto, IUpdateCommand<LightSensorReading, long>
    {
    }
}
