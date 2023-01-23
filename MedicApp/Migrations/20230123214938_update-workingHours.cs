using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicApp.Migrations
{
    public partial class updateworkingHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFriday",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "IsMonday",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "IsSaturday",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "IsThursday",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "IsTuesday",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "IsWednesday",
                table: "WorkingHours");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "WorkingHours",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "WorkingHours");

            migrationBuilder.AddColumn<bool>(
                name: "IsFriday",
                table: "WorkingHours",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMonday",
                table: "WorkingHours",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSaturday",
                table: "WorkingHours",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsThursday",
                table: "WorkingHours",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTuesday",
                table: "WorkingHours",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWednesday",
                table: "WorkingHours",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
