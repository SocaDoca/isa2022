using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicApp.Migrations
{
    public partial class firsttime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "Penalty",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Questionnaire",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Appointment2Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Appointment_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    ReportId = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment2Reports", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment2Reports");

            migrationBuilder.DropColumn(
                name: "Penalty",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Questionnaire");

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "Appointments",
                type: "longtext",
                nullable: false);
        }
    }
}
