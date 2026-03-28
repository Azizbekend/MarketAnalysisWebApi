using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAnalysisWebApi.Migrations
{
    /// <inheritdoc />
    public partial class fix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DryPumps_ProjectRequestsTable_RequestId",
                table: "DryPumps");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmersiblePumps_ProjectRequestsTable_RequestId",
                table: "SubmersiblePumps");

            migrationBuilder.AlterColumn<Guid>(
                name: "RequestId",
                table: "SubmersiblePumps",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "RequestId",
                table: "DryPumps",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_DryPumps_ProjectRequestsTable_RequestId",
                table: "DryPumps",
                column: "RequestId",
                principalTable: "ProjectRequestsTable",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmersiblePumps_ProjectRequestsTable_RequestId",
                table: "SubmersiblePumps",
                column: "RequestId",
                principalTable: "ProjectRequestsTable",
                principalColumn: "Id");
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

            migrationBuilder.AlterColumn<Guid>(
                name: "RequestId",
                table: "SubmersiblePumps",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RequestId",
                table: "DryPumps",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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
    }
}
