using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicApp.Migrations
{
    public partial class updateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Start",
                table: "WorkingHours",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<int>(
                name: "End",
                table: "WorkingHours",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<string>(
                name: "JMBG",
                table: "Users",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Job",
                table: "Users",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Users",
                type: "longtext",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "Appointment2Clinics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Appointment_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    Clinic_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment2Clinics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointment2Doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Appointment_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    Doctor_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment2Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointment2Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Appointment_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    Patient_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment2Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "char(36)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AppointmentStatus = table.Column<int>(type: "int", nullable: false),
                    ChangedByUser_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: true),
                    PlannedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment2Clinics");

            migrationBuilder.DropTable(
                name: "Appointment2Doctors");

            migrationBuilder.DropTable(
                name: "Appointment2Patients");

            migrationBuilder.DropTable(
                name: "AppointmentHistories");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropColumn(
                name: "JMBG",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Job",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Start",
                table: "WorkingHours",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "End",
                table: "WorkingHours",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
