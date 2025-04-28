using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimRegisPortal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProjectPermission");

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DateFrom = table.Column<DateTime>(type: "date", nullable: false),
                    DateTo = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_EmployeeId",
                table: "Contract",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ProjectId",
                table: "Contract",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.CreateTable(
                name: "UserProjectPermission",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionType = table.Column<int>(type: "int", nullable: false),
                    ProjectId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProjectPermission", x => new { x.UserId, x.ProjectId, x.PermissionType });
                    table.ForeignKey(
                        name: "FK_UserProjectPermission_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProjectPermission_Project_ProjectId1",
                        column: x => x.ProjectId1,
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProjectPermission_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectPermission_ProjectId",
                table: "UserProjectPermission",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectPermission_ProjectId1",
                table: "UserProjectPermission",
                column: "ProjectId1");
        }
    }
}
