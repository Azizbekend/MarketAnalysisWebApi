using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAnalysisWebApi.Migrations
{
    /// <inheritdoc />
    public partial class pumpConfigsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RequestId",
                table: "DbPumpConfiguration",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DbPumpConfiguration_RequestId",
                table: "DbPumpConfiguration",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_DbPumpConfiguration_ProjectRequestsTable_RequestId",
                table: "DbPumpConfiguration",
                column: "RequestId",
                principalTable: "ProjectRequestsTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbPumpConfiguration_ProjectRequestsTable_RequestId",
                table: "DbPumpConfiguration");

            migrationBuilder.DropIndex(
                name: "IX_DbPumpConfiguration_RequestId",
                table: "DbPumpConfiguration");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "DbPumpConfiguration");
        }
    }
}
