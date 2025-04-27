using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimRegisPortal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixExchangeRatePrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "ExchangeRate",
                type: "decimal(18,7)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,10)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "ExchangeRate",
                type: "decimal(18,10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,7)");
        }
    }
}
