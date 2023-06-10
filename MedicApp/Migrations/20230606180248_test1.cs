using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicApp.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "WorkItems");

            migrationBuilder.AddColumn<int>(
                name: "WorkItemType",
                table: "WorkItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkItemType",
                table: "WorkItems");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WorkItems",
                type: "longtext",
                nullable: false);
        }
    }
}
