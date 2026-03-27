using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAnalysisWebApi.Migrations
{
    /// <inheritdoc />
    public partial class RegionsTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RegionId",
                table: "ProjectRequestsTable",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RegionsTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RegionName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionsTable", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRequestsTable_RegionId",
                table: "ProjectRequestsTable",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequestsTable_RegionsTable_RegionId",
                table: "ProjectRequestsTable",
                column: "RegionId",
                principalTable: "RegionsTable",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequestsTable_RegionsTable_RegionId",
                table: "ProjectRequestsTable");

            migrationBuilder.DropTable(
                name: "RegionsTable");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRequestsTable_RegionId",
                table: "ProjectRequestsTable");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "ProjectRequestsTable");
        }
    }
}
