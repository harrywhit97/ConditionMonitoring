using ConditionMonitoringAPI.Features.SensorsReadings.Validators;
using Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ConditionMonitoringAPI.Tests
{
    [TestClass]
    public class LightSensorReadingTests
    {
        Mock<IDateTime> DateTime;
        DateTimeOffset DateTimeDefualt;
        ConditionMonitoringDbContext Context;
        LightSensorReadingValidator Validator;

        [TestInitialize]
        public void Initialize()
        {
            DateTimeDefualt = new DateTimeOffset(2020, 04, 04, 13, 12, 11, TimeSpan.Zero);
            DateTime = Utils.Utils.GetMockDateTime(DateTimeDefualt);
            Context = DbContextMocker.GetConditionMonitoringDbContextMock("ConditionMonitoring", DateTime.Object);

            Validator = new LightSensorReadingValidator();
        }
    }
}
