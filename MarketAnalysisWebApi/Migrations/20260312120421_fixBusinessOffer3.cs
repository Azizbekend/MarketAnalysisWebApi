using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAnalysisWebApi.Migrations
{
    /// <inheritdoc />
    public partial class fixBusinessOffer3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CurrentPriceNoNDS",
                table: "OffersTable",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ManufacturerCountry",
                table: "OffersTable",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OffersNumber",
                table: "OffersTable",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SupportingDocumentDate",
                table: "OffersTable",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPriceNoNDS",
                table: "OffersTable");

            migrationBuilder.DropColumn(
                name: "ManufacturerCountry",
                table: "OffersTable");

            migrationBuilder.DropColumn(
                name: "OffersNumber",
                table: "OffersTable");

            migrationBuilder.DropColumn(
                name: "SupportingDocumentDate",
                table: "OffersTable");
        }
    }
}
