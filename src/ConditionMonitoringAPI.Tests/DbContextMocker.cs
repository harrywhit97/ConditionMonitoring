using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConditionMonitoringAPI.Tests
{
    public static class DbContextMocker
    {
        public static ConditionMonitoringDbContext GetConditionMonitoringDbContextMock(string dbname, IDateTime dateTime)
        {
            var options = new DbContextOptionsBuilder<ConditionMonitoringDbContext>()
                .UseInMemoryDatabase(databaseName: dbname)
                .Options;

            var dbContext = new ConditionMonitoringDbContext(options, dateTime);

            dbContext.Seed();

            return dbContext;
        }
    }
}
