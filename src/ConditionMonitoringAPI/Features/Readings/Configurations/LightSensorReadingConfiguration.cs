using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConditionMonitoringAPI.Features.Readings
{
    public class LightSensorReadingConfiguration : IEntityTypeConfiguration<LightSensorReading>
    {
        public void Configure(EntityTypeBuilder<LightSensorReading> builder)
        {
            builder.Property(x => x.ReadingTime)
                .IsRequired();

            builder.Property(x => x.RawReading)
                .HasColumnType("decimal (18,2)")
                .IsRequired();
        }
    }
}
