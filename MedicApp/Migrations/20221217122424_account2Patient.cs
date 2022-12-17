using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicApp.Migrations
{
    public partial class account2Patient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Accounts");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Patients",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Account2Clinics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Account_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    Clinic_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account2Clinics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account2Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Account_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    Patient_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account2Patients", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account2Clinics");

            migrationBuilder.DropTable(
                name: "Account2Patients");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Accounts",
                type: "longtext",
                nullable: false);
        }
    }
}
