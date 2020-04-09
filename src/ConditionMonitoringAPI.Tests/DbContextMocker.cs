using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConditionMonitoringAPI.Tests
{
    public static class DbContextMocker
    {
        public static ConditionMonitoringDbContext GetConditionMonitoringDbContext(IDateTime dateTime)
        {
            var options = new DbContextOptionsBuilder<ConditionMonitoringDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new ConditionMonitoringDbContext(options, dateTime);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }
    }
}
