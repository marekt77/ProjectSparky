using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectSparky.Model.Migrations
{
    public partial class AlarmUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Alarms",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Alarms");
        }
    }
}
