using AutoMapper;
using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Features.Readings;
using Domain.Interfaces;
using FluentValidation.Results;
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

        public Mock<TValidator> GetValidatorMock<TValidator, T>(bool result = true) 
            where TValidator : AbstractValidatorWrapper<T>
            where T : class
        {
            var validatorResultMock = new Mock<ValidationResult>();
            validatorResultMock.Setup(x => x.IsValid).Returns(result);

            var validatorMock = new Mock<TValidator>();
            validatorMock.Setup(x => x.Validate(It.IsAny<T>())).Returns(validatorResultMock.Object);

            return validatorMock;
        }
    }
}
