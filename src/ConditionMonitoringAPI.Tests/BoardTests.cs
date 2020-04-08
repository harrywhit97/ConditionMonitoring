using AutoMapper;
using ConditionMonitoringAPI.Exceptions;
using ConditionMonitoringAPI.Features.Boards;
using ConditionMonitoringAPI.Features.Boards.Validators;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using ConditionMonitoringAPI.Features.Readings;
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
using static ConditionMonitoringAPI.Features.Boards.BoardHandlers;

namespace ConditionMonitoringAPI.Tests
{
    [TestClass]
    public class BoardTestsTests
    {
        Mock<IDateTime> DateTime;
        DateTimeOffset DateTimeDefualt;
        ConditionMonitoringDbContext Context;
        CancellationToken CancToken => new CancellationToken();
        Mock<ILogger> Logger;
        IMapper Mapper;

        [TestInitialize]
        public void Initialize()
        {
            DateTimeDefualt = new DateTimeOffset(2020, 04, 04, 13, 12, 11, TimeSpan.Zero);
            DateTime = Utils.Utils.GetMockDateTime(DateTimeDefualt);
            Context = DbContextMocker.GetConditionMonitoringDbContextMock("ConditionMonitoring", DateTime.Object);

            Logger = new Mock<ILogger>();

            var myProfile = new FeaturesProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            Mapper = new Mapper(configuration);
        }

        [TestMethod]
        public void GetExistingBoardByIdSucceeds()
        {
            //Arrange
            var handler = new GetBoardByIdHandler(Context, Logger.Object, Mapper);
            var query = new GetEntityById<Board, long>(1);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void GetNonExistingBoardByIdFailsWithException()
        {
            //Arrange
            var handler = new GetBoardByIdHandler(Context, Logger.Object, Mapper);
            var query = new GetEntityById<Board, long>(0);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<Exception>().WithMessage("Could not find a Board with an Id of 0");
        }

        [TestMethod]
        public void CreateBoardSucceeds()
        {
            //Arrange
            var entity = new BoardDto()
            {
                Name = "testname",
                IpAddress = "testIp"
            };

            var validatorResultMock = new Mock<ValidationResult>();
            validatorResultMock.Setup(x => x.IsValid).Returns(true);

            var validatorMock = new Mock<BoardValidator>();
            validatorMock.Setup(x => x.Validate(It.IsAny<Board>())).Returns(validatorResultMock.Object);

            var handler = new CreateBoardHandler(Context, Logger.Object, validatorMock.Object, Mapper);
            var query = new CreateEntityFromDto<Board, long, BoardDto>(entity);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(entity.Name);
            result.IpAddress.Should().Be(entity.IpAddress);
        }

        [TestMethod]
        public void CreateBoardWithValidationErrorThrowsException()
        {
            //Arrange
            var entity = new BoardDto();

            var validatorResultMock = new Mock<ValidationResult>();
            validatorResultMock.Setup(x => x.IsValid).Returns(false);

            var validatorMock = new Mock<BoardValidator>();
            validatorMock.Setup(x => x.Validate(It.IsAny<Board>())).Returns(validatorResultMock.Object);

            var handler = new CreateBoardHandler(Context, Logger.Object, validatorMock.Object, Mapper);
            var query = new CreateEntityFromDto<Board, long, BoardDto>(entity);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<Exception>();
        }

        [TestMethod]
        public void DeleteExistingBoardSucceeds()
        {
            //Arrange
            var entity = new Board() { Id = 42};

            Context.Set<Board>().Add(entity);
            Context.SaveChanges();
            
            var handler = new DeleteBoardByIdHandler(Context, Logger.Object, Mapper);
            var query = new DeleteEntity<Board, long>(42);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void DeleteNonExistingBoardFailsWithException()
        {
            //Arrange
            var handler = new DeleteBoardByIdHandler(Context, Logger.Object, Mapper);
            var query = new DeleteEntity<Board, long>(42);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<RestException>().Which
                .StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
