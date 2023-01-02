using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicApp.Migrations
{
    public partial class WorkingHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clinic2WorkingHours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Clinic_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    WorkingHours_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic2WorkingHours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    End = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsMonday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsTuesday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsWednesday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsThursday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsFriday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsSaturday = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clinic2WorkingHours");

            migrationBuilder.DropTable(
                name: "WorkingHours");
        }
    }
}
