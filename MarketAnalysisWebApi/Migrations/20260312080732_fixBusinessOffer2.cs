using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAnalysisWebApi.Migrations
{
    /// <inheritdoc />
    public partial class fixBusinessOffer2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RequestId",
                table: "OffersTable",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OffersTable_RequestId",
                table: "OffersTable",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_OffersTable_ProjectRequestsTable_RequestId",
                table: "OffersTable",
                column: "RequestId",
                principalTable: "ProjectRequestsTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OffersTable_ProjectRequestsTable_RequestId",
                table: "OffersTable");

            migrationBuilder.DropIndex(
                name: "IX_OffersTable_RequestId",
                table: "OffersTable");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "OffersTable");
        }
    }
}
