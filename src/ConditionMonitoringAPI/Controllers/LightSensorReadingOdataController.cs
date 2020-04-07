using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Validators;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Controllers
{
    public class LightSensorReadingOdataController : ReadOnlyController<LightSensorReading, long>
    {
        public LightSensorReadingOdataController(ConditionMonitoringDbContext context)
            :base(context)
        {
        }
    }
}
