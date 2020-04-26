using AutoMapper;
using Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace ConditionMonitoringAPI.Tests
{
    public abstract class AbstractTestClass : IDisposable
    {
        public Mock<IDateTime> DateTime;
        public DateTimeOffset DateTimeDefualt;
        public ConditionMonitoringDbContext Context;
        public CancellationToken CancToken => new CancellationToken();
        public IMapper Mapper;

        [TestInitialize]
        public virtual void Initialize()
        {
            DateTimeDefualt = new DateTimeOffset(2020, 04, 04, 13, 12, 11, TimeSpan.Zero);
            DateTime = Utils.Utils.GetMockDateTime(DateTimeDefualt);
            Context = DbContextMocker.GetConditionMonitoringDbContext(DateTime.Object);
        }


        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }

        public abstract void Seed(ConditionMonitoringDbContext dbContext);
    }
}
