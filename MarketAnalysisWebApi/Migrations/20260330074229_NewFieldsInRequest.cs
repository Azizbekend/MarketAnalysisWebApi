using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAnalysisWebApi.Migrations
{
    /// <inheritdoc />
    public partial class NewFieldsInRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ObjectStage",
                table: "ProjectRequestsTable",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectDocsChapter",
                table: "ProjectRequestsTable",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationEndDate",
                table: "ProjectRequestsTable",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "GarantyPeriod",
                table: "OffersTable",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObjectStage",
                table: "ProjectRequestsTable");

            migrationBuilder.DropColumn(
                name: "ProjectDocsChapter",
                table: "ProjectRequestsTable");

            migrationBuilder.DropColumn(
                name: "PublicationEndDate",
                table: "ProjectRequestsTable");

            migrationBuilder.AlterColumn<string>(
                name: "GarantyPeriod",
                table: "OffersTable",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
