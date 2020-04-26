using ConditionMonitoringAPI.Services;
using Moq;
using System;

namespace ConditionMonitoringAPI.Tests.Utils
{
    public static class Utils
    {
        public static Mock<IDateTime> GetMockDateTime(DateTimeOffset dateTime)
        {
            var dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(x => x.Now)
                .Returns(dateTime);
            return dateTimeMock;
        }
    }
}
