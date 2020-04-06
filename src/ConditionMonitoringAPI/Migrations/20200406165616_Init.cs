using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConditionMonitoringAPI.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<long>(nullable: false),
                    BoardId = table.Column<long>(nullable: true),
                    Pin = table.Column<long>(nullable: false),
                    CommsType = table.Column<long>(nullable: false),
                    SensorType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                    table.CheckConstraint("CK_Sensors_SensorType_Enum_Constraint", "[SensorType] IN(0)");
                    table.ForeignKey(
                        name: "FK_Sensors_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LightSensorReadings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SensorId = table.Column<long>(nullable: true),
                    ReadingTime = table.Column<DateTimeOffset>(nullable: false),
                    RawReading = table.Column<decimal>(maxLength: 5, nullable: false),
                    Brightness = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LightSensorReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LightSensorReadings_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LightSensorReadings_SensorId",
                table: "LightSensorReadings",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_BoardId",
                table: "Sensors",
                column: "BoardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LightSensorReadings");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "Boards");
        }
    }
}
