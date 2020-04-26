using ConditionMonitoringAPI.Exceptions;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using ConditionMonitoringAPI.Features.Sensors.Commands;
using ConditionMonitoringAPI.Features.Sensors.Dtos;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using static ConditionMonitoringAPI.Features.Sensors.Commands.DeleteSensor;
using static ConditionMonitoringAPI.Features.Sensors.Queries.GetSensorQueries;

namespace ConditionMonitoringAPI.Tests
{
    [TestClass]
    public class SensorTests : AbstractTestClass
    {
        [TestMethod]
        public void GetExistingSensorByIdSucceeds()
        {
            //Arrange
            var logger = new Mock<ILogger<GetSensorByIdHandler>>();


            var handler = new GetSensorByIdHandler(Context);
            var query = new GetEntityById<Sensor<ISensorReading>, long>(1);

            Seed(Context);
            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void GetNonExistingSensorByIdFailsWithException()
        {
            //Arrange
            var logger = new Mock<ILogger<GetSensorByIdHandler>>();

            var handler = new GetSensorByIdHandler(Context);
            var query = new GetEntityById<Sensor<ISensorReading>, long>(0);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<NotFoundException>()
                .WithMessage("Entity \"Sensor`1\" (0) was not found.");
        }

        [TestMethod]
        public void CreateSensorSucceeds()
        {
            //Arrange
            var request = new CreateSensor()
            {
                Name = "testName",
                Address = 255,
                SensorType = SensorType.Light,
                Pin = 456
            };

            var handler = new CreateSensorHandler(Context, Mapper);

            //Act
            var result = handler.Handle(request, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
            result.Pin.Should().Be(request.Pin);
            result.SensorType.Should().Be(request.SensorType);
        }

        [TestMethod]
        public void DeleteExistingSensorSucceeds()
        {
            //Arrange
            var entity = new Sensor<ISensorReading>() { Id = 42};
            var context = Context;
            context.Set<Sensor<ISensorReading>>().Add(entity);
            context.SaveChanges();

            var handler = new DeleteSensorHandler(Context);
            var query = new DeleteEntity<Sensor<ISensorReading>, long>(42);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void DeleteNonExistingSensorFailsWithException()
        {
            //Arrange
            var handler = new DeleteSensorHandler(Context);
            var query = new DeleteEntity<Sensor<ISensorReading>, long>(42);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<NotFoundException>()
                .WithMessage("Entity \"Sensor`1\" (42) was not found.");
        }

        [TestMethod]
        public void UpdateExistingSensorSucceeds()
        {
            //Arrange
            var query = new UpdateSensor() { Id = 1, Name = "updatedname", BoardId = 2 };
            var handler = new UpdateSensorHandler(Context, Mapper);

            Seed(Context);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(query.Name);
        }

        public override void Seed(ConditionMonitoringDbContext dbContext)
        {
            var board = new Board()
            {
                Id = 2
            };
            dbContext.Add(board);

            var sensors = new[]
            {
                new Sensor<ISensorReading>()
                {
                    Id = 1,
                    Name = "AName",
                    Pin = 123,
                    Board = board
                }
            };
            dbContext.AddRange(sensors);
            dbContext.SaveChanges();
        }
    }
}
