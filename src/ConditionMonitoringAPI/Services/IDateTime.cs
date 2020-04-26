using System;

namespace ConditionMonitoringAPI.Services
{
    public interface IDateTime
    {
        DateTimeOffset Now { get; }
    }
}
