using Microsoft.EntityFrameworkCore.Migrations;

namespace ConditionMonitoringAPI.Migrations
{
    public partial class _260420 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "BoardId",
                table: "Sensors",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "BoardId",
                table: "Sensors",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));
        }
    }
}
