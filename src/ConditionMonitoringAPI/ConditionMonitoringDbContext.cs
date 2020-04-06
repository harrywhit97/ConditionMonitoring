﻿using Domain.Abstract;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI
{
    public class ConditionMonitoringDbContext : DbContext
    {
        
        public DbSet<LightSensorReading> LightSensorReadings { get; set; }
        public DbSet<LightSensor> Sensors { get; set; }
        public DbSet<Board> Boards { get; set; }
        readonly IDateTime DateTime;

        public ConditionMonitoringDbContext(DbContextOptions<ConditionMonitoringDbContext> options, IDateTime dateTime)
            : base(options)
        {
            DateTime = dateTime;
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

            modelbuilder.Entity<LightSensor>()
                .HasMany(s => s.Readings)
                .WithOne(r => r.Sensor)
                .OnDelete(DeleteBehavior.Cascade);

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

        //ModelBuilder ConfigSensorAndReading<TSensor, TReading>(ModelBuilder modelbuilder) 
        //    where TSensor : class, ISensor<TReading> 
        //    where TReading : class, ISensorReading<TSensor>
        //{
        //    modelbuilder.Entity<TReading>().HasOne<Board>();
        //    modelbuilder.Entity<LightSensorReading>().HasOne(r => r.Sensor as LightSensor);
        //    modelbuilder.Entity<TSensor>().HasOne<Board>();

        //    return modelbuilder;
        //}
    }
}
