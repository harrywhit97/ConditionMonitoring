using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConditionMonitoringAPI.Features.Sensors.Configurations
{
    public class SensorConfiguration : IEntityTypeConfiguration<Sensor<ISensorReading>>
    {
        public void Configure(EntityTypeBuilder<Sensor<ISensorReading>> builder)
        {
            builder.Property(x => x.Pin)
                .IsRequired();
        }
    }
}
