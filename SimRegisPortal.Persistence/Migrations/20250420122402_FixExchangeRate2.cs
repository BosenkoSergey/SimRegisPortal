using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimRegisPortal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixExchangeRate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeRate",
                table: "ExchangeRate");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangeRate",
                table: "ExchangeRate",
                columns: new[] { "FromCurrencyId", "ToCurrencyId", "Date" });
        }
    }
}
