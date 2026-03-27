using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAnalysisWebApi.Migrations
{
    /// <inheritdoc />
    public partial class pumpConfigsUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RequestId",
                table: "SubmersiblePumps",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RequestId",
                table: "DryPumps",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SubmersiblePumps_RequestId",
                table: "SubmersiblePumps",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_DryPumps_RequestId",
                table: "DryPumps",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_DryPumps_ProjectRequestsTable_RequestId",
                table: "DryPumps",
                column: "RequestId",
                principalTable: "ProjectRequestsTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmersiblePumps_ProjectRequestsTable_RequestId",
                table: "SubmersiblePumps",
                column: "RequestId",
                principalTable: "ProjectRequestsTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DryPumps_ProjectRequestsTable_RequestId",
                table: "DryPumps");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmersiblePumps_ProjectRequestsTable_RequestId",
                table: "SubmersiblePumps");

            migrationBuilder.DropIndex(
                name: "IX_SubmersiblePumps_RequestId",
                table: "SubmersiblePumps");

            migrationBuilder.DropIndex(
                name: "IX_DryPumps_RequestId",
                table: "DryPumps");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "SubmersiblePumps");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "DryPumps");
        }
    }
}
