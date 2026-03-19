using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAnalysisWebApi.Migrations
{
    /// <inheritdoc />
    public partial class ModelsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryTerms",
                table: "OffersTable",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GarantyPeriod",
                table: "OffersTable",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentTerms",
                table: "OffersTable",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryTerms",
                table: "OffersTable");

            migrationBuilder.DropColumn(
                name: "GarantyPeriod",
                table: "OffersTable");

            migrationBuilder.DropColumn(
                name: "PaymentTerms",
                table: "OffersTable");
        }
    }
}
