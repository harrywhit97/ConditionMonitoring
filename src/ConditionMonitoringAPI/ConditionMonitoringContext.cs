using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI
{
    public class ConditionMonitoringContext : DbContext
    {
        
        public DbSet<LightSensorReading> LightSensorReadings { get; set; }

        public ConditionMonitoringContext(DbContextOptions<ConditionMonitoringContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
        }
    }
}
