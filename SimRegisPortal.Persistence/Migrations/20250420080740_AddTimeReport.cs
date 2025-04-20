using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimRegisPortal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "EmployeeActivity");

            migrationBuilder.DropColumn(
                name: "IsFinalized",
                table: "EmployeeActivity");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "EmployeeActivity");

            migrationBuilder.AddColumn<Guid>(
                name: "TimeReportId",
                table: "EmployeeActivity",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TimeReport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeReport_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeActivity_TimeReportId",
                table: "EmployeeActivity",
                column: "TimeReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeReport_EmployeeId_Year_Month",
                table: "TimeReport",
                columns: new[] { "EmployeeId", "Year", "Month" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeActivity_TimeReport_TimeReportId",
                table: "EmployeeActivity",
                column: "TimeReportId",
                principalTable: "TimeReport",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeActivity_TimeReport_TimeReportId",
                table: "EmployeeActivity");

            migrationBuilder.DropTable(
                name: "TimeReport");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeActivity_TimeReportId",
                table: "EmployeeActivity");

            migrationBuilder.DropColumn(
                name: "TimeReportId",
                table: "EmployeeActivity");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "EmployeeActivity",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinalized",
                table: "EmployeeActivity",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "EmployeeActivity",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
