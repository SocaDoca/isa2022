using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicApp.Migrations
{
    public partial class aspnetIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("3f89d518-a178-4c2c-ab24-62207d0492c2"), false, "User" },
                    { new Guid("5c5ab713-5a6f-4e7c-ae12-9a7f2d9f2d2a"), false, "SysAdmin" },
                    { new Guid("61d9ddcc-19eb-4854-90eb-3205f9448a9a"), false, "Guest" },
                    { new Guid("bdc1239a-f831-41e3-b05c-e0cba50ff544"), false, "CenterAdmin" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
