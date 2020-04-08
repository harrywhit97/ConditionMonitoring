using AutoMapper;
using ConditionMonitoringAPI.Exceptions;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using ConditionMonitoringAPI.Features.Readings;
using ConditionMonitoringAPI.Features.Readings.Dtos;
using ConditionMonitoringAPI.Features.SensorsReadings.Validators;
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

namespace ConditionMonitoringAPI.Tests
{
    [TestClass]
    public class LightSensorReadingTests
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
        public void GetExistingLightSensorReadingByIdSucceeds()
        {
            //Arrange

            var logger = new Mock<ILogger<GetLightSensorReadingByIdHandler>>();

            var handler = new GetLightSensorReadingByIdHandler(Context, logger.Object, Mapper);
            var query = new GetEntityById<LightSensorReading, long>(1);

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

            var handler = new GetLightSensorReadingByIdHandler(Context, logger.Object, Mapper);
            var query = new GetEntityById<LightSensorReading, long>(0);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<Exception>().WithMessage("Could not find a LightSensorReading with an Id of 0");
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

            var validatorResultMock = new Mock<ValidationResult>();
            validatorResultMock.Setup(x => x.IsValid).Returns(true);

            var validatorMock = new Mock<LightSensorReadingValidator>();
            validatorMock.Setup(x => x.Validate(It.IsAny<LightSensorReading>())).Returns(validatorResultMock.Object);
            
            var logger = new Mock<ILogger<CreateLightSensorReadingHandler>>();

            var handler = new CreateLightSensorReadingHandler(Context, logger.Object, validatorMock.Object, Mapper);
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

            var validatorResultMock = new Mock<ValidationResult>();
            validatorResultMock.Setup(x => x.IsValid).Returns(false);

            var validatorMock = new Mock<LightSensorReadingValidator>();
            validatorMock.Setup(x => x.Validate(It.IsAny<LightSensorReading>())).Returns(validatorResultMock.Object);
            
            var logger = new Mock<ILogger<CreateLightSensorReadingHandler>>();

            var handler = new CreateLightSensorReadingHandler(Context, logger.Object, validatorMock.Object, Mapper);
            var query = new CreateEntityFromDto<LightSensorReading, long, LightSensorReadingDto>(entity);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<Exception>();
        }

        [TestMethod]
        public void DeleteExistingLightSensorReadingSucceeds()
        {
            //Arrange
            var entity = new LightSensorReading() { Id = 42};

            Context.Set<LightSensorReading>().Add(entity);
            Context.SaveChanges();

            var logger = new Mock<ILogger<DeleteLightSensorReadingByIdHandler>>();

            var handler = new DeleteLightSensorReadingByIdHandler(Context, logger.Object, Mapper);
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

            var handler = new DeleteLightSensorReadingByIdHandler(Context, logger.Object, Mapper);
            var query = new DeleteEntity<LightSensorReading, long>(42);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<RestException>().Which
                .StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
