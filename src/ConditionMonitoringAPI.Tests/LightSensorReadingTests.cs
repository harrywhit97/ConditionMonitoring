using ConditionMonitoringAPI.Exceptions;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using ConditionMonitoringAPI.Features.Readings;
using ConditionMonitoringAPI.Features.Readings.Dtos;
using ConditionMonitoringAPI.Features.SensorsReadings.Validators;
using Domain.Interfaces;
using Domain.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;

namespace ConditionMonitoringAPI.Tests
{
    [TestClass]
    public class LightSensorReadingTests : AbstractTestClass
    {

        [TestMethod]
        public void GetExistingLightSensorReadingByIdSucceeds()
        {
            //Arrange
            var logger = new Mock<ILogger<GetLightSensorReadingByIdHandler>>();

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
            act.Should().Throw<RestException>()
                .WithMessage("Could not find a LightSensorReading with an Id of 0")
                .And
                .StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void CreateLightSensorReadingSucceeds()
        {
            //Arrange
            var entity = new LightSensorReadingDto()
            {
                RawReading = 255,
                Brightness = 123
            };

            var validatorMock = GetValidatorMock<LightSensorReadingValidator, LightSensorReading>();
            var logger = new Mock<ILogger<CreateLightSensorReadingHandler>>();

            var handler = new CreateLightSensorReadingHandler(Context, validatorMock.Object, Mapper);
            var query = new CreateEntityFromDto<LightSensorReading, long, LightSensorReadingDto>(entity);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
            result.RawReading.Should().Be(entity.RawReading);
            result.Brightness.Should().Be(entity.Brightness);
        }

        [TestMethod]
        public void CreateLightSensorReadingWithValidationErrorThrowsException()
        {
            //Arrange
            var entity = new LightSensorReadingDto();

            var validatorMock = GetValidatorMock<LightSensorReadingValidator, LightSensorReading>(false);
            var logger = new Mock<ILogger<CreateLightSensorReadingHandler>>();

            var handler = new CreateLightSensorReadingHandler(Context, validatorMock.Object, Mapper);
            var query = new CreateEntityFromDto<LightSensorReading, long, LightSensorReadingDto>(entity);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<RestException>()
                .WithMessage("Exception of type 'FluentValidation.ValidationException' was thrown.")
                .And
                .StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void DeleteExistingLightSensorReadingSucceeds()
        {
            //Arrange
            var entity = new LightSensorReading() { Id = 42};

            Context.Set<LightSensorReading>().Add(entity);
            Context.SaveChanges();

            var logger = new Mock<ILogger<DeleteLightSensorReadingByIdHandler>>();

            var handler = new DeleteLightSensorReadingByIdHandler(Context);
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
            var logger = new Mock<ILogger<DeleteLightSensorReadingByIdHandler>>();

            var handler = new DeleteLightSensorReadingByIdHandler(Context);
            var query = new DeleteEntity<LightSensorReading, long>(42);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<RestException>()
                .WithMessage("Could not find a LightSensorReading with an Id of '42'")
                .And
                .StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void UpdateExistingLightSensorReadingSucceeds()
        {
            //Arrange
            var dto = new LightSensorReadingDto() { Brightness = 999, SensorId = 2 };

            var logger = new Mock<ILogger<UpdateLightSensorReadingHandler>>();
            var validatorMock = GetValidatorMock<LightSensorReadingValidator, LightSensorReading>();

            var handler = new UpdateLightSensorReadingHandler(Context, logger.Object, validatorMock.Object, Mapper);
            var query = new UpdateEntityFromDto<LightSensorReading, long, LightSensorReadingDto>(1, dto);

            Seed(Context);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
            result.Brightness.Should().Be(dto.Brightness);
        }

        [TestMethod]
        public void UpdateExistingLightSensorReadingWithBadSensorIdReadingFaildWithException()
        {
            //Arrange
            var dto = new LightSensorReadingDto() { Brightness = 999, SensorId = 3 };

            var logger = new Mock<ILogger<UpdateLightSensorReadingHandler>>();
            var validatorMock = GetValidatorMock<LightSensorReadingValidator, LightSensorReading>();

            var handler = new UpdateLightSensorReadingHandler(Context, logger.Object, validatorMock.Object, Mapper);
            var query = new UpdateEntityFromDto<LightSensorReading, long, LightSensorReadingDto>(1, dto);

            Seed(Context);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<RestException>()
                .WithMessage("Could not find a sensor with an Id of 3")
                .And.StatusCode.Should().Be(HttpStatusCode.BadRequest);
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
