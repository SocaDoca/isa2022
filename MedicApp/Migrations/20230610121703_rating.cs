using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicApp.Migrations
{
    public partial class rating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClinicRating2Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ClinicId = table.Column<Guid>(type: "char(36)", nullable: false),
                    PatientId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Value = table.Column<double>(type: "double", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicRating2Patients", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicRating2Patients");
        }
    }
}
