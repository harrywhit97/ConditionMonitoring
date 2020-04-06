﻿// <auto-generated />
using System;
using ConditionMonitoringAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConditionMonitoringAPI.Migrations
{
    [DbContext(typeof(ConditionMonitoringDbContext))]
    partial class ConditionMonitoringDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-preview.2.20159.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Models.Board", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("IpAddress")
                        .IsUnique();

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("Domain.Models.LightSensorReading", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Brightness")
                        .HasColumnType("int");

                    b.Property<decimal>("RawReading")
                        .HasColumnType("decimal(18,2)")
                        .HasMaxLength(5);

                    b.Property<DateTimeOffset>("ReadingTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<long?>("SensorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.ToTable("LightSensorReadings");
                });

            modelBuilder.Entity("Domain.Models.Sensor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Address")
                        .HasColumnType("bigint");

                    b.Property<long?>("BoardId")
                        .HasColumnType("bigint");

                    b.Property<long>("CommsType")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Pin")
                        .HasColumnType("bigint");

                    b.Property<int>("SensorType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("Sensors");

                    b.HasCheckConstraint("CK_Sensors_SensorType_Enum_Constraint", "[SensorType] IN(0)");
                });

            modelBuilder.Entity("Domain.Models.LightSensorReading", b =>
                {
                    b.HasOne("Domain.Models.Sensor", "Sensor")
                        .WithMany("Readings")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Models.Sensor", b =>
                {
                    b.HasOne("Domain.Models.Board", "Board")
                        .WithMany("Sensors")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
