using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicApp.Migrations
{
    public partial class updateuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patient2Questionnaires",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Patient_RefId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Questionnaire_RefId = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient2Questionnaires", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaire",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
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
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaire", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patient2Questionnaires");

            migrationBuilder.DropTable(
                name: "Questionnaire");
        }
    }
}
