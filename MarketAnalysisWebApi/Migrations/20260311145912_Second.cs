using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAnalysisWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OffersTable_OfferFilesTable_OfferFileId",
                table: "OffersTable");

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "ProjectRequestsTable",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ProjectRequestsTable",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "OfferFileId",
                table: "OffersTable",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_OffersTable_OfferFilesTable_OfferFileId",
                table: "OffersTable",
                column: "OfferFileId",
                principalTable: "OfferFilesTable",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OffersTable_OfferFilesTable_OfferFileId",
                table: "OffersTable");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "ProjectRequestsTable");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProjectRequestsTable");

            migrationBuilder.AlterColumn<Guid>(
                name: "OfferFileId",
                table: "OffersTable",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OffersTable_OfferFilesTable_OfferFileId",
                table: "OffersTable",
                column: "OfferFileId",
                principalTable: "OfferFilesTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
