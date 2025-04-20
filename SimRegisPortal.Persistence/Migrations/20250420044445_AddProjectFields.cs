using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimRegisPortal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId1",
                table: "UserProjectPermission",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Project",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Project",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Project",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Project",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Project",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectPermission_ProjectId1",
                table: "UserProjectPermission",
                column: "ProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjectPermission_Project_ProjectId1",
                table: "UserProjectPermission",
                column: "ProjectId1",
                principalTable: "Project",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProjectPermission_Project_ProjectId1",
                table: "UserProjectPermission");

            migrationBuilder.DropIndex(
                name: "IX_UserProjectPermission_ProjectId1",
                table: "UserProjectPermission");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "UserProjectPermission");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Project");
        }
    }
}
