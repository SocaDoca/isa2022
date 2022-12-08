using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicApp.Migrations
{
    public partial class GenderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Employees");
        }
    }
}
