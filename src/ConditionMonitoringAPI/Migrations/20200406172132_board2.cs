using Microsoft.EntityFrameworkCore.Migrations;

namespace ConditionMonitoringAPI.Migrations
{
    public partial class board2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "Boards",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boards_IpAddress",
                table: "Boards",
                column: "IpAddress",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Boards_IpAddress",
                table: "Boards");

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "Boards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
