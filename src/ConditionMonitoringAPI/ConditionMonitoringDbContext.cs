using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApiUtilities.Abstract;
using WebApiUtilities.Interfaces;

namespace ConditionMonitoringAPI
{
    public class ConditionMonitoringDbContext : AuditingDbContext
    {        
        public DbSet<LightSensorReading> LightSensorReadings { get; set; }
        public DbSet<Sensor<ISensorReading>> Sensors { get; set; }
        public DbSet<Board> Boards { get; set; }

        public ConditionMonitoringDbContext(DbContextOptions<ConditionMonitoringDbContext> options, IClock clock)
            : base(options, clock)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfigurationsFromAssembly(typeof(ConditionMonitoringDbContext).Assembly);

            modelbuilder.Entity<Board>()
                .HasMany(b => b.Sensors)
                .WithOne(s => s.Board)
                .OnDelete(DeleteBehavior.Cascade);

            modelbuilder.Entity<Board>()
                .HasIndex(b => b.IpAddress)
                .IsUnique();

            modelbuilder.Entity<LightSensorReading>()
                .HasOne(r => r.Sensor)
                .WithMany(s => s.Readings as ICollection<LightSensorReading>)
                .OnDelete(DeleteBehavior.Cascade);

            modelbuilder.Entity<LightSensorReading>()
                .HasOne(r => r.Sensor)
                .WithMany(s => s.Readings as ICollection<LightSensorReading>)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelbuilder);
        }        
    }
}
