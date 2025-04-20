using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimRegisPortal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixExchangeRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeRate",
                table: "ExchangeRate");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRate_FromCurrencyId_ToCurrencyId_Date",
                table: "ExchangeRate");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ExchangeRate");

            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "ExchangeRate",
                type: "decimal(18,10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangeRate",
                table: "ExchangeRate",
                columns: new[] { "FromCurrencyId", "ToCurrencyId", "Date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeRate",
                table: "ExchangeRate");

            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "ExchangeRate",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,10)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ExchangeRate",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangeRate",
                table: "ExchangeRate",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRate_FromCurrencyId_ToCurrencyId_Date",
                table: "ExchangeRate",
                columns: new[] { "FromCurrencyId", "ToCurrencyId", "Date" },
                unique: true);
        }
    }
}
