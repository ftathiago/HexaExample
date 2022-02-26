using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HexaEmployee.EfInfraData.Migrations
{
    public partial class CreateEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMPLOYEE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    NAME = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    EMAIL = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    SALARY = table.Column<decimal>(type: "MONEY", nullable: false),
                    SALARY_RAISE_DATE = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMPLOYEE");
        }
    }
}
