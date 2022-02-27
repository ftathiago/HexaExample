using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HexaEmployee.EfInfraData.Migrations
{
    public partial class CreateSalaryHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SALARY",
                table: "EMPLOYEE");

            migrationBuilder.DropColumn(
                name: "SALARY_RAISE_DATE",
                table: "EMPLOYEE");

            migrationBuilder.CreateTable(
                name: "EMPLOYEE_SALARY_HISTORY",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    EMPLOYEE_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    SALARY = table.Column<decimal>(type: "MONEY", nullable: false),
                    RAISED_AT = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE_SALARY_HISTORY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EMPLOYEE_SALARY_HISTORY_EMPLOYEE_EMPLOYEE_ID",
                        column: x => x.EMPLOYEE_ID,
                        principalTable: "EMPLOYEE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_SALARY_HISTORY_EMPLOYEE_ID",
                table: "EMPLOYEE_SALARY_HISTORY",
                column: "EMPLOYEE_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMPLOYEE_SALARY_HISTORY");

            migrationBuilder.AddColumn<decimal>(
                name: "SALARY",
                table: "EMPLOYEE",
                type: "MONEY",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "SALARY_RAISE_DATE",
                table: "EMPLOYEE",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
