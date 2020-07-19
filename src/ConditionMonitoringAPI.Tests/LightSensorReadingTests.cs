using Domain.Interfaces;
using Domain.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ConditionMonitoringAPI.Tests
{
    [TestClass]
    public class LightSensorReadingTests : AbstractTestClass
    {

        [TestMethod]
        public void GetExistingLightSensorReadingByIdSucceeds()
        {
            //Arrange
            var handler = new GetLightSensorReadingByIdHandler(Context);
            var query = new GetEntityById<LightSensorReading, long>(1);

            Seed(Context);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void GetNonExistingLightSensorReadingByIdFailsWithException()
        {
            //Arrange
            var logger = new Mock<ILogger<GetLightSensorReadingByIdHandler>>();

            var handler = new GetLightSensorReadingByIdHandler(Context);
            var query = new GetEntityById<LightSensorReading, long>(0);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<NotFoundException>()
                .WithMessage("Entity \"LightSensorReading\" (0) was not found.");
        }

        [TestMethod]
        public void CreateLightSensorReadingSucceeds()
        {
            //Arrange
            var query = new CreateLightSensorReading()
            {
                RawReading = 255,
                Brightness = 123
            };

            var handler = new CreateLightSensorReadingHandler(Context, Mapper);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
            result.RawReading.Should().Be(query.RawReading);
            result.Brightness.Should().Be(query.Brightness);
        }

        [TestMethod]
        public void DeleteExistingLightSensorReadingSucceeds()
        {
            //Arrange
            var entity = new LightSensorReading() { Id = 42};

            Context.Set<LightSensorReading>().Add(entity);
            Context.SaveChanges();

            var handler = new DeleteLightSensorReadingHandler(Context);
            var query = new DeleteEntity<LightSensorReading, long>(42);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void DeleteNonExistingLightSensorReadingFailsWithException()
        {
            //Arrange
            var handler = new DeleteLightSensorReadingHandler(Context);
            var query = new DeleteEntity<LightSensorReading, long>(42);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<NotFoundException>()
                .WithMessage("Entity \"LightSensorReading\" (42) was not found.");
        }

        [TestMethod]
        public void UpdateExistingLightSensorReadingSucceeds()
        {
            //Arrange
            var query = new UpdateLightSensorReading() { Id = 1, Brightness = 999, SensorId = 2 };

            var handler = new UpdateLightSensorReadingHandler(Context, Mapper);

            Seed(Context);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
            result.Brightness.Should().Be(query.Brightness);
        }

        public override void Seed(ConditionMonitoringDbContext dbContext)
        {
            var sensor = new Sensor<ISensorReading>()
            {
                Id = 2
            };

            dbContext.Add(sensor);

            var readings = new[]
            {
                new LightSensorReading()
                {
                    Id = 1,
                    RawReading = 255,
                    Brightness = 123,
                    Sensor = sensor
                }
            };
            dbContext.AddRange(readings);
            dbContext.SaveChanges();
        }
    }
}
