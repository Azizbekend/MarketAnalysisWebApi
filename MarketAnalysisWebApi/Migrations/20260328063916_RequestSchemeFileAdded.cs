using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAnalysisWebApi.Migrations
{
    /// <inheritdoc />
    public partial class RequestSchemeFileAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationRegion",
                table: "ProjectRequestsTable");

            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "ProjectRequestsTable",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IntakeType",
                table: "DbPumpConfiguration",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReservePumpsCount",
                table: "DbPumpConfiguration",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkPumpsCount",
                table: "DbPumpConfiguration",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RequestFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    ContentType = table.Column<string>(type: "text", nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    FileData = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestFiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRequestsTable_FileId",
                table: "ProjectRequestsTable",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequestsTable_RequestFiles_FileId",
                table: "ProjectRequestsTable",
                column: "FileId",
                principalTable: "RequestFiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequestsTable_RequestFiles_FileId",
                table: "ProjectRequestsTable");

            migrationBuilder.DropTable(
                name: "RequestFiles");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRequestsTable_FileId",
                table: "ProjectRequestsTable");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "ProjectRequestsTable");

            migrationBuilder.DropColumn(
                name: "IntakeType",
                table: "DbPumpConfiguration");

            migrationBuilder.DropColumn(
                name: "ReservePumpsCount",
                table: "DbPumpConfiguration");

            migrationBuilder.DropColumn(
                name: "WorkPumpsCount",
                table: "DbPumpConfiguration");

            migrationBuilder.AddColumn<string>(
                name: "LocationRegion",
                table: "ProjectRequestsTable",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
