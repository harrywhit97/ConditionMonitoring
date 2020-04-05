using Domain.Interfaces;
using System;

namespace ConditionMonitoringAPI.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}
