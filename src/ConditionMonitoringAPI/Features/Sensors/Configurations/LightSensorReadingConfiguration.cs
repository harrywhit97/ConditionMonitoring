using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConditionMonitoringAPI.Features.Sensors.Configurations
{
    public class LightSensorReadingConfiguration : IEntityTypeConfiguration<LightSensorReading>
    {
        public void Configure(EntityTypeBuilder<LightSensorReading> builder)
        {
            builder.Property(x => x.RawReading)
                .HasMaxLength(5)
                .IsRequired();
        }
    }
}
