using ConditionMonitoringAPI.Exceptions;
using ConditionMonitoringAPI.Features.Boards.Commands;
using ConditionMonitoringAPI.Features.Common.Commands;
using ConditionMonitoringAPI.Features.Common.Queries;
using Domain.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static ConditionMonitoringAPI.Features.Boards.Commands.DeleteSensor;
using static ConditionMonitoringAPI.Features.Boards.Queries.GetBoardQueries;

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
            act.Should().Throw<NotFoundException>()
                .WithMessage("Entity \"Board\" (0) was not found.");
        }

        [TestMethod]
        public void CreateBoardSucceeds()
        {
            //Arrange
            var query = new CreateBoard()
            {
                Name = "testname",
                IpAddress = "testIp"
            };

            var handler = new CreateBoardHandler(Context, Mapper);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(query.Name);
            result.IpAddress.Should().Be(query.IpAddress);
        }
        
        [TestMethod]
        public void DeleteExistingBoardSucceeds()
        {
            //Arrange
            var entity = new Board() { Id = 42};

            Context.Set<Board>().Add(entity);
            Context.SaveChanges();

            var handler = new DeleteBoardHandler(Context);
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
            var handler = new DeleteBoardHandler(Context);
            var query = new DeleteEntity<Board, long>(42);

            //Act
            Action act = () => handler.Handle(query, CancToken).Result.Should();

            //Assert
            act.Should().Throw<NotFoundException>();
        }

        [TestMethod]
        public void UpdateExistingBoardSucceeds()
        {
            //Arrange
            var query = new UpdateBoard() { Id = 1, Name = "updatedName", IpAddress = "updatedIp" };
            var handler = new UpdateBoardHandler(Context, Mapper);

            Seed(Context);

            //Act
            var result = handler.Handle(query, CancToken).Result;

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(query.Name);
            result.IpAddress.Should().Be(query.IpAddress);
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
