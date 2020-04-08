using AutoMapper;
using ConditionMonitoringAPI.Exceptions;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using ConditionMonitoringAPI.Features.Readings;
using ConditionMonitoringAPI.Features.Sensors;
using ConditionMonitoringAPI.Features.Sensors.Dtos;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using System.Threading;
using static ConditionMonitoringAPI.Features.Sensors.SensorHandlers;

namespace ConditionMonitoringAPI.Tests
{
    [TestClass]
    public class SensorTests
    {
        Mock<IDateTime> DateTime;
        DateTimeOffset DateTimeDefualt;
        ConditionMonitoringDbContext Context;
        CancellationToken CancToken => new CancellationToken();
        IMapper Mapper;

        [TestInitialize]
        public void Initialize()
        {
            DateTimeDefualt = new DateTimeOffset(2020, 04, 04, 13, 12, 11, TimeSpan.Zero);
            DateTime = Utils.Utils.GetMockDateTime(DateTimeDefualt);
            Context = DbContextMocker.GetConditionMonitoringDbContextMock("ConditionMonitoring", DateTime.Object);

            var myProfile = new FeaturesProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            Mapper = new Mapper(configuration);
        }

        [TestMethod]
        public void GetExistingSensorByIdSucceeds()
        {
            //Arrange
            var logger = new Mock<ILogger<GetSensorByIdHandler>>();


            var handler = new GetSensorByIdHandler(Context, logger.Object, Mapper);
            var query = new GetEntityById<Sensor<ISensorReading>, long>(1);

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
            act.Should().Throw<Exception>().WithMessage("Could not find a Sensor`1 with an Id of 0");
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

            var validatorResultMock = new Mock<ValidationResult>();
            validatorResultMock.Setup(x => x.IsValid).Returns(true);

            var validatorMock = new Mock<SensorValidator>();
            validatorMock.Setup(x => x.Validate(It.IsAny<Sensor<ISensorReading>>())).Returns(validatorResultMock.Object);

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

            var validatorResultMock = new Mock<ValidationResult>();
            validatorResultMock.Setup(x => x.IsValid).Returns(false);

            var validatorMock = new Mock<SensorValidator>();
            validatorMock.Setup(x => x.Validate(It.IsAny<Sensor<ISensorReading>>())).Returns(validatorResultMock.Object);

            var logger = new Mock<ILogger<CreateSensorHandler>>();

            var handler = new CreateSensorHandler(Context, logger.Object, validatorMock.Object, Mapper);
            var query = new CreateEntityFromDto<Sensor<ISensorReading>, long, SensorDto>(entity);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<Exception>();
        }

        [TestMethod]
        public void DeleteExistingSensorSucceeds()
        {
            //Arrange
            var entity = new Sensor<ISensorReading>() { Id = 42};

            Context.Set<Sensor<ISensorReading>>().Add(entity);
            Context.SaveChanges();

            var logger = new Mock<ILogger<DeleteSensorByIdHandler>>();

            var handler = new DeleteSensorByIdHandler(Context, logger.Object, Mapper);
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
            act.Should().Throw<RestException>().Which
                .StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
