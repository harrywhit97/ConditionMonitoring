using ConditionMonitoringAPI.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConditionMonitoringAPI.Tests
{
    public static class DbContextExtensions
    {
        public static void Seed(this ConditionMonitoringDbContext context)
        {
            var helper = new LightSensorReadingHelper();
            context.Add(helper.GetNewReading());
            context.SaveChangesAsync();
        }
    }
}
