using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAnalysisWebApi.Migrations
{
    /// <inheritdoc />
    public partial class fixBusinessOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OffersTable_CompaniesTable_CompanyId",
                table: "OffersTable");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "OffersTable",
                newName: "BussinessAccId");

            migrationBuilder.RenameIndex(
                name: "IX_OffersTable_CompanyId",
                table: "OffersTable",
                newName: "IX_OffersTable_BussinessAccId");

            migrationBuilder.AddColumn<Guid>(
                name: "DbCompanyId",
                table: "OffersTable",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OffersTable_DbCompanyId",
                table: "OffersTable",
                column: "DbCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_OffersTable_BusinessAccounts_BussinessAccId",
                table: "OffersTable",
                column: "BussinessAccId",
                principalTable: "BusinessAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OffersTable_CompaniesTable_DbCompanyId",
                table: "OffersTable",
                column: "DbCompanyId",
                principalTable: "CompaniesTable",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OffersTable_BusinessAccounts_BussinessAccId",
                table: "OffersTable");

            migrationBuilder.DropForeignKey(
                name: "FK_OffersTable_CompaniesTable_DbCompanyId",
                table: "OffersTable");

            migrationBuilder.DropIndex(
                name: "IX_OffersTable_DbCompanyId",
                table: "OffersTable");

            migrationBuilder.DropColumn(
                name: "DbCompanyId",
                table: "OffersTable");

            migrationBuilder.RenameColumn(
                name: "BussinessAccId",
                table: "OffersTable",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_OffersTable_BussinessAccId",
                table: "OffersTable",
                newName: "IX_OffersTable_CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_OffersTable_CompaniesTable_CompanyId",
                table: "OffersTable",
                column: "CompanyId",
                principalTable: "CompaniesTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
