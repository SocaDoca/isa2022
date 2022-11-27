using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicApp.Migrations
{
    public partial class updatemodelsv2 : Migration
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
                name: "IsSunday",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "IsThursday",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "IsTuesday",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "Wednesday",
                table: "WorkingHours",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "LoyaltyLevel",
                table: "Patients",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Appointments",
                newName: "StartAt");

            migrationBuilder.AlterColumn<int>(
                name: "WorkDuration",
                table: "WorkingHours",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AddColumn<int>(
                name: "WorkDay",
                table: "WorkingHours",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Job",
                table: "Patients",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Employees",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Clinic2AdminCenters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Clinic_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    AdminCenter_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic2AdminCenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clinic2Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Clinic_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    Employee_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic2Employees", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clinic2AdminCenters");

            migrationBuilder.DropTable(
                name: "Clinic2Employees");

            migrationBuilder.DropColumn(
                name: "WorkDay",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "Job",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "WorkingHours",
                newName: "Wednesday");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Patients",
                newName: "LoyaltyLevel");

            migrationBuilder.RenameColumn(
                name: "StartAt",
                table: "Appointments",
                newName: "StartDate");

            migrationBuilder.AlterColumn<double>(
                name: "WorkDuration",
                table: "WorkingHours",
                type: "double",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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
                name: "IsSunday",
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

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "Appointments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Addresses",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Addresses",
                type: "longtext",
                nullable: false);
        }
    }
}
