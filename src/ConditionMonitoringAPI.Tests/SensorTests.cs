using ConditionMonitoringAPI.Exceptions;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using ConditionMonitoringAPI.Features.Sensors;
using ConditionMonitoringAPI.Features.Sensors.Dtos;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using static ConditionMonitoringAPI.Features.Sensors.SensorHandlers;

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


            var handler = new GetSensorByIdHandler(Context, logger.Object, Mapper);
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

            var handler = new GetSensorByIdHandler(Context, logger.Object, Mapper);
            var query = new GetEntityById<Sensor<ISensorReading>, long>(0);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<RestException>()
                .WithMessage("Could not find a Sensor`1 with an Id of 0")
                .And
                .StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void CreateSensorSucceeds()
        {
            //Arrange
            var entity = new SensorDto()
            {
                Name = "testName",
                Address = 255,
                SensorType = SensorType.Light,
                Pin = 456
            };

            var validatorMock = GetValidatorMock<SensorValidator, Sensor<ISensorReading>>();
            var logger = new Mock<ILogger<CreateSensorHandler>>();

            var handler = new CreateSensorHandler(Context, logger.Object, validatorMock.Object, Mapper);
            var query = new CreateEntityFromDto<Sensor<ISensorReading>, long, SensorDto>(entity);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(entity.Name);
            result.Pin.Should().Be(entity.Pin);
            result.SensorType.Should().Be(entity.SensorType);
        }

        [TestMethod]
        public void CreateSensorWithValidationErrorThrowsException()
        {
            //Arrange
            var entity = new SensorDto();

            var validatorMock = GetValidatorMock<SensorValidator, Sensor<ISensorReading>>(false);

            var logger = new Mock<ILogger<CreateSensorHandler>>();

            var handler = new CreateSensorHandler(Context, logger.Object, validatorMock.Object, Mapper);
            var query = new CreateEntityFromDto<Sensor<ISensorReading>, long, SensorDto>(entity);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<RestException>()
                .WithMessage("Exception of type 'FluentValidation.ValidationException' was thrown.")
                .And
                .StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void DeleteExistingSensorSucceeds()
        {
            //Arrange
            var entity = new Sensor<ISensorReading>() { Id = 42};
            var context = Context;
            context.Set<Sensor<ISensorReading>>().Add(entity);
            context.SaveChanges();

            var logger = new Mock<ILogger<DeleteSensorByIdHandler>>();

            var handler = new DeleteSensorByIdHandler(context, logger.Object, Mapper);
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

            var logger = new Mock<ILogger<DeleteSensorByIdHandler>>();

            var handler = new DeleteSensorByIdHandler(Context, logger.Object, Mapper);
            var query = new DeleteEntity<Sensor<ISensorReading>, long>(42);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<RestException>()
                .WithMessage("Could not find a Sensor`1 with an Id of '42'")
                .And
                .StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void UpdateExistingSensorSucceeds()
        {
            //Arrange
            var dto = new SensorDto() { Name = "updatedname", BoardId = 2 };

            var logger = new Mock<ILogger<UpdateSensorHandler>>();
            var validatorMock = GetValidatorMock<SensorValidator, Sensor<ISensorReading>>();

            var handler = new UpdateSensorHandler(Context, logger.Object, validatorMock.Object, Mapper);
            var query = new UpdateEntityFromDto<Sensor<ISensorReading>, long, SensorDto>(1, dto);

            Seed(Context);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(dto.Name);
        }

        [TestMethod]
        public void UpdateExistingSensorWithBadBoardIdReadingFailsWithException()
        {
            //Arrange
            var dto = new SensorDto() { Name = "updatedname", BoardId = 3 };

            var logger = new Mock<ILogger<UpdateSensorHandler>>();
            var validatorMock = GetValidatorMock<SensorValidator, Sensor<ISensorReading>>();

            var handler = new UpdateSensorHandler(Context, logger.Object, validatorMock.Object, Mapper);
            var query = new UpdateEntityFromDto<Sensor<ISensorReading>, long, SensorDto>(1, dto);

            Seed(Context);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<RestException>()
                .WithMessage("Could not find a board with an Id of 3")
                .And.StatusCode.Should().Be(HttpStatusCode.BadRequest);
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
