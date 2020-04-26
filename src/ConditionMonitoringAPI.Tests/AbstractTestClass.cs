using AutoMapper;
using ConditionMonitoringAPI.Mapping;
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
        IConfigurationProvider _configuration;

        [TestInitialize]
        public virtual void Initialize()
        {
            DateTimeDefualt = new DateTimeOffset(2020, 04, 04, 13, 12, 11, TimeSpan.Zero);
            DateTime = Utils.Utils.GetMockDateTime(DateTimeDefualt);
            Context = DbContextMocker.GetConditionMonitoringDbContext(DateTime.Object);

            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = _configuration.CreateMapper();
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            Mapper = new Mapper(configuration);
        }


        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }

        public abstract void Seed(ConditionMonitoringDbContext dbContext);
    }
}
