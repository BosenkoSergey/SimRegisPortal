using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimRegisPortal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTaxSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalCurrencyId = table.Column<int>(type: "int", nullable: false),
                    MinimumWage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SocialTax = table.Column<decimal>(type: "decimal(18,7)", nullable: false),
                    Fop2Pit = table.Column<decimal>(type: "decimal(18,7)", nullable: false),
                    Fop2MilitaryTax = table.Column<decimal>(type: "decimal(18,7)", nullable: false),
                    Fop3Pit = table.Column<decimal>(type: "decimal(18,7)", nullable: false),
                    Fop3MilitaryTax = table.Column<decimal>(type: "decimal(18,7)", nullable: false),
                    GigPit = table.Column<decimal>(type: "decimal(18,7)", nullable: false),
                    GigMilitaryTax = table.Column<decimal>(type: "decimal(18,7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxSetting_Currency_LocalCurrencyId",
                        column: x => x.LocalCurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxSetting_LocalCurrencyId",
                table: "TaxSetting",
                column: "LocalCurrencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxSetting");
        }
    }
}
