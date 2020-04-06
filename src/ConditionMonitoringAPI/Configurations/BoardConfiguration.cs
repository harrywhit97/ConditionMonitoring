using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConditionMonitoringAPI.Configurations
{
    public class BoardConfiguration : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(50);

            builder.Property(x => x.IpAddress)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
