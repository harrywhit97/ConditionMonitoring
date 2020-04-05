using Domain.Abstract;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI
{
    public class ConditionMonitoringDbContext : DbContext
    {
        
        public DbSet<LightSensorReading> LightSensorReadings { get; set; }
        readonly IDateTime DateTime;

        public ConditionMonitoringDbContext(DbContextOptions<ConditionMonitoringDbContext> options, IDateTime dateTime)
            : base(options)
        {
            DateTime = dateTime;
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfigurationsFromAssembly(typeof(ConditionMonitoringDbContext).Assembly);
            base.OnModelCreating(modelbuilder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
