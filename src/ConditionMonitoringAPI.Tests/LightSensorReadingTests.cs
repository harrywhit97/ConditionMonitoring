using ConditionMonitoringAPI.Controllers;
using ConditionMonitoringAPI.Validators;
using Domain.Interfaces;
using Domain.Models;
using FluentAssertions;
using FluentValidation.Results;
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
            var controller = new LightSensorReadingOdataController(Context);

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
            var controller = new LightSensorReadingOdataController(Context);

            //Act
            var response = controller.Get(0);

            //Assert
            response.Should().NotBeNull();
        }
    }
}
