using ConditionMonitoringAPI.Features.Readings.Controllers;
using ConditionMonitoringAPI.Features.Sensors.Validators;
using Domain.Interfaces;
using FluentAssertions;
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

        [TestMethod]
        public void TestGetLightSensorReadings()
        {
            //Arrange
            var controller = new LightSensorReadingController(Context, Validator);

            //Act
            var response = controller.Get();

            //Assert
            response.Should().NotBeNull();
            response.Should().NotBeEmpty();
        }

        [TestMethod]
        public void TestGetLightSensorReadingById()
        {
            //Arrange
            var controller = new LightSensorReadingController(Context, Validator);

            //Act
            var response = controller.Get(0);

            //Assert
            response.Should().NotBeNull();
        }
    }
}
