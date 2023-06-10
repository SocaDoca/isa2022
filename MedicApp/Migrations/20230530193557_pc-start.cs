using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicApp.Migrations
{
    public partial class pcstart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account2Clinics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Account_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    Clinic_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account2Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointment2Clinics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Appointment_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    Clinic_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment2Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointment2Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Appointment_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    ReportId = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment2Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "char(36)", nullable: true),
                    IsStartedAppointment = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsFinishedAppointment = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ChangedByUser_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Clinic_RefID = table.Column<Guid>(type: "char(36)", nullable: true),
                    Patient_RefID = table.Column<Guid>(type: "char(36)", nullable: true),
                    IsCanceled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsStarted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsPredefiend = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsFinished = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsReserved = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentsReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentsReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clinic2Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Clinic_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    Address_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic2Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clinic2Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Clinic_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    Employee_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic2Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clinic2WorkingHours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Clinic_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    WorkingHours_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic2WorkingHours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    WorksFrom = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    WorksTo = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Address = table.Column<string>(type: "longtext", nullable: false),
                    City = table.Column<string>(type: "longtext", nullable: false),
                    Country = table.Column<string>(type: "longtext", nullable: false),
                    Phone = table.Column<string>(type: "longtext", nullable: false),
                    Rating = table.Column<double>(type: "double", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Complaint2Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Patient_RefId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Complaint_RefId = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaint2Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsForClinic = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsForEmployee = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsForClinic_Clinic_RefId = table.Column<Guid>(type: "char(36)", nullable: true),
                    IsForEmployee_User_RefId = table.Column<Guid>(type: "char(36)", nullable: true),
                    UserInput = table.Column<string>(type: "longtext", nullable: false),
                    Answer = table.Column<string>(type: "longtext", nullable: false),
                    IsAnswered = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ComplaintBy_User_RefId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee2WokringHours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Employee_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    WorkingHours_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee2WokringHours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Patient_RefId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Clinic_RefId = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaire",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Patient_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    question1 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    question2 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    question3 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    question4 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    question5 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    question6 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    question7 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    question8 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    question9 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    question10 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    question11 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    question12 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsValid = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaire", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FirstName = table.Column<string>(type: "longtext", nullable: true),
                    LastName = table.Column<string>(type: "longtext", nullable: true),
                    Username = table.Column<string>(type: "longtext", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    Mobile = table.Column<string>(type: "longtext", nullable: true),
                    JMBG = table.Column<string>(type: "longtext", nullable: true),
                    Address = table.Column<string>(type: "longtext", nullable: true),
                    Country = table.Column<string>(type: "longtext", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    LoyaltyPoints = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "longtext", nullable: true),
                    Role = table.Column<string>(type: "longtext", nullable: true),
                    Job = table.Column<string>(type: "longtext", nullable: true),
                    IsAdminCenter = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "longblob", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "longblob", nullable: false),
                    IsVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsFirstTime = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Penalty = table.Column<int>(type: "int", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkItem2Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    WorkItem_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    Report_RefID = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItem2Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    UsedInstances = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creation_TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AppointmentReportId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkItems_AppointmentsReports_AppointmentReportId",
                        column: x => x.AppointmentReportId,
                        principalTable: "AppointmentsReports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_AppointmentReportId",
                table: "WorkItems",
                column: "AppointmentReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account2Clinics");

            migrationBuilder.DropTable(
                name: "Account2Patients");

            migrationBuilder.DropTable(
                name: "Appointment2Clinics");

            migrationBuilder.DropTable(
                name: "Appointment2Doctors");

            migrationBuilder.DropTable(
                name: "Appointment2Patients");

            migrationBuilder.DropTable(
                name: "Appointment2Reports");

            migrationBuilder.DropTable(
                name: "AppointmentHistories");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Clinic2Addresses");

            migrationBuilder.DropTable(
                name: "Clinic2Employees");

            migrationBuilder.DropTable(
                name: "Clinic2WorkingHours");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "Complaint2Patients");

            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.DropTable(
                name: "Employee2WokringHours");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Questionnaire");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkItem2Reports");

            migrationBuilder.DropTable(
                name: "WorkItems");

            migrationBuilder.DropTable(
                name: "AppointmentsReports");
        }
    }
}
