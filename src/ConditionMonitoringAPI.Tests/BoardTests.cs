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
    public class BoardTests : AbstractTestClass
    {
        [TestMethod]
        public void GetExistingBoardByIdSucceeds()
        {
            //Arrange
            var handler = new GetBoardByIdHandler(Context);
            var query = new GetEntityById<Board, long>(1);

            Seed(Context);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void GetNonExistingBoardByIdFailsWithException()
        {
            //Arrange
            var handler = new GetBoardByIdHandler(Context);
            var query = new GetEntityById<Board, long>(0);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<RestException>()
                .WithMessage("Could not find a Board with an Id of 0")
                .And
                .StatusCode.Should().Be(HttpStatusCode.NotFound);
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

            var validatorMock = GetValidatorMock<BoardValidator, Board>();

            var logger = new Mock<ILogger<CreateBoardHandler>>();

            var handler = new CreateBoardHandler(Context, validatorMock.Object, Mapper);
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

            var validatorMock = GetValidatorMock<BoardValidator, Board>(false);
            var logger = new Mock<ILogger<CreateBoardHandler>>();

            var handler = new CreateBoardHandler(Context, validatorMock.Object, Mapper);
            var query = new CreateEntityFromDto<Board, long, BoardDto>(entity);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<RestException>()
                .WithMessage("Exception of type 'FluentValidation.ValidationException' was thrown.")
                .And
                .StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void DeleteExistingBoardSucceeds()
        {
            //Arrange
            var entity = new Board() { Id = 42};

            Context.Set<Board>().Add(entity);
            Context.SaveChanges();

            var logger = new Mock<ILogger<DeleteBoardByIdHandler>>();

            var handler = new DeleteBoardByIdHandler(Context);
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
            var logger = new Mock<ILogger<DeleteBoardByIdHandler>>();

            var handler = new DeleteBoardByIdHandler(Context);
            var query = new DeleteEntity<Board, long>(42);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<RestException>().Which
                .StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void UpdateExistingBoardSucceeds()
        {
            //Arrange
            var dto = new BoardDto() { Name = "updatedName", IpAddress = "updatedIp" };

            var logger = new Mock<ILogger<UpdateBoardHandler>>();
            var validatorMock = GetValidatorMock<BoardValidator, Board>();

            var handler = new UpdateBoardHandler(Context, validatorMock.Object, Mapper);
            var query = new UpdateEntityFromDto<Board, long, BoardDto>(1, dto);

            Seed(Context);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(dto.Name);
            result.IpAddress.Should().Be(dto.IpAddress);
        }

        public override void Seed(ConditionMonitoringDbContext dbContext)
        {
            var boards = new[]
            {
                new Board()
                {
                    Id = 1,
                    Name = "AName",
                    IpAddress = "ip"
                }
            };
            dbContext.AddRange(boards);
            dbContext.SaveChanges();
        }
    }
}
