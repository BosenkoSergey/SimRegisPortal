using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimRegisPortal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixTimeReportActivities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeActivity_TimeReport_TimeReportId",
                table: "EmployeeActivity");

            migrationBuilder.AlterColumn<Guid>(
                name: "TimeReportId",
                table: "EmployeeActivity",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeActivity_TimeReport_TimeReportId",
                table: "EmployeeActivity",
                column: "TimeReportId",
                principalTable: "TimeReport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeActivity_TimeReport_TimeReportId",
                table: "EmployeeActivity");

            migrationBuilder.AlterColumn<Guid>(
                name: "TimeReportId",
                table: "EmployeeActivity",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeActivity_TimeReport_TimeReportId",
                table: "EmployeeActivity",
                column: "TimeReportId",
                principalTable: "TimeReport",
                principalColumn: "Id");
        }
    }
}
