using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using ConditionMonitoringAPI.Features.SensorsReadings.Validators;
using Domain.Interfaces;
using Domain.Models;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace ConditionMonitoringAPI.Tests
{
    [TestClass]
    public class CrossCuttingTests
    {
        Mock<IDateTime> DateTime;
        DateTimeOffset DateTimeDefualt;
        ConditionMonitoringDbContext Context;
        CancellationToken CancToken => new CancellationToken();
        Mock<ILogger> Logger;

        [TestInitialize]
        public void Initialize()
        {
            DateTimeDefualt = new DateTimeOffset(2020, 04, 04, 13, 12, 11, TimeSpan.Zero);
            DateTime = Utils.Utils.GetMockDateTime(DateTimeDefualt);
            Context = DbContextMocker.GetConditionMonitoringDbContextMock("ConditionMonitoring", DateTime.Object);
            
            Logger = new Mock<ILogger>();
        }

        [TestMethod]
        public void GetExistingEntityByIdSucceeds()
        {
            //Arrange
            var handler = new GetByIdHandler<LightSensorReading, long>(Context, Logger.Object);
            var query = new GetById<LightSensorReading, long>(1);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void GetNonExistingEntityByIdFailsWithException()
        {
            //Arrange
            var handler = new GetByIdHandler<LightSensorReading, long>(Context, Logger.Object);
            var query = new GetById<LightSensorReading, long>(0);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<Exception>().WithMessage("Could not find a LightSensorReading with an Id of 0");
        }

        [TestMethod]
        public void CreateEntitySucceeds()
        {
            //Arrange
            var entity = new LightSensorReading()
            {
                RawReading = 255,
                Brightness = 123
            };

            var validatorResultMock = new Mock<ValidationResult>();
            validatorResultMock.Setup(x => x.IsValid).Returns(true);

            var validatorMock = new Mock<LightSensorReadingValidator>();
            validatorMock.Setup(x => x.Validate(It.IsAny<LightSensorReading>())).Returns(validatorResultMock.Object);

            var handler = new CreateEntityHandler<LightSensorReading, long, LightSensorReadingValidator>(Context, Logger.Object, validatorMock.Object);
            var query = new CreateEntity<LightSensorReading, long>(entity);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
            result.RawReading.Should().Be(entity.RawReading);
            result.Brightness.Should().Be(entity.Brightness);
        }

        [TestMethod]
        public void CreateEntityWithValidationErrorThrowsException()
        {
            //Arrange
            var entity = new LightSensorReading();
            
            var validatorResultMock = new Mock<ValidationResult>();
            validatorResultMock.Setup(x => x.IsValid).Returns(false);

            var validatorMock = new Mock<LightSensorReadingValidator>();
            validatorMock.Setup(x => x.Validate(It.IsAny<LightSensorReading>())).Returns(validatorResultMock.Object);

            var handler = new CreateEntityHandler<LightSensorReading, long, LightSensorReadingValidator>(Context, Logger.Object, validatorMock.Object);
            var query = new CreateEntity<LightSensorReading, long>(entity);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<Exception>();
        }
    }
}
