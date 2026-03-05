using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAnalysisWebApi.Migrations
{
    /// <inheritdoc />
    public partial class FileStorages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompaniesTable_DbImageFileModel_LogoFileId",
                table: "CompaniesTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbImageFileModel",
                table: "DbImageFileModel");

            migrationBuilder.RenameTable(
                name: "DbImageFileModel",
                newName: "ImageFilesTable");

            migrationBuilder.AddColumn<Guid>(
                name: "CertificateFileId",
                table: "OffersTable",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OfferFileId",
                table: "OffersTable",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PassportFileId",
                table: "OffersTable",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanFileId",
                table: "OffersTable",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageFilesTable",
                table: "ImageFilesTable",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CertificatesFilesTable",
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
                    table.PrimaryKey("PK_CertificatesFilesTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferFilesTable",
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
                    table.PrimaryKey("PK_OfferFilesTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PassportsFilesTable",
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
                    table.PrimaryKey("PK_PassportsFilesTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanFilesTable",
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
                    table.PrimaryKey("PK_PlanFilesTable", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OffersTable_CertificateFileId",
                table: "OffersTable",
                column: "CertificateFileId");

            migrationBuilder.CreateIndex(
                name: "IX_OffersTable_OfferFileId",
                table: "OffersTable",
                column: "OfferFileId");

            migrationBuilder.CreateIndex(
                name: "IX_OffersTable_PassportFileId",
                table: "OffersTable",
                column: "PassportFileId");

            migrationBuilder.CreateIndex(
                name: "IX_OffersTable_PlanFileId",
                table: "OffersTable",
                column: "PlanFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompaniesTable_ImageFilesTable_LogoFileId",
                table: "CompaniesTable",
                column: "LogoFileId",
                principalTable: "ImageFilesTable",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OffersTable_CertificatesFilesTable_CertificateFileId",
                table: "OffersTable",
                column: "CertificateFileId",
                principalTable: "CertificatesFilesTable",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OffersTable_OfferFilesTable_OfferFileId",
                table: "OffersTable",
                column: "OfferFileId",
                principalTable: "OfferFilesTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OffersTable_PassportsFilesTable_PassportFileId",
                table: "OffersTable",
                column: "PassportFileId",
                principalTable: "PassportsFilesTable",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OffersTable_PlanFilesTable_PlanFileId",
                table: "OffersTable",
                column: "PlanFileId",
                principalTable: "PlanFilesTable",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompaniesTable_ImageFilesTable_LogoFileId",
                table: "CompaniesTable");

            migrationBuilder.DropForeignKey(
                name: "FK_OffersTable_CertificatesFilesTable_CertificateFileId",
                table: "OffersTable");

            migrationBuilder.DropForeignKey(
                name: "FK_OffersTable_OfferFilesTable_OfferFileId",
                table: "OffersTable");

            migrationBuilder.DropForeignKey(
                name: "FK_OffersTable_PassportsFilesTable_PassportFileId",
                table: "OffersTable");

            migrationBuilder.DropForeignKey(
                name: "FK_OffersTable_PlanFilesTable_PlanFileId",
                table: "OffersTable");

            migrationBuilder.DropTable(
                name: "CertificatesFilesTable");

            migrationBuilder.DropTable(
                name: "OfferFilesTable");

            migrationBuilder.DropTable(
                name: "PassportsFilesTable");

            migrationBuilder.DropTable(
                name: "PlanFilesTable");

            migrationBuilder.DropIndex(
                name: "IX_OffersTable_CertificateFileId",
                table: "OffersTable");

            migrationBuilder.DropIndex(
                name: "IX_OffersTable_OfferFileId",
                table: "OffersTable");

            migrationBuilder.DropIndex(
                name: "IX_OffersTable_PassportFileId",
                table: "OffersTable");

            migrationBuilder.DropIndex(
                name: "IX_OffersTable_PlanFileId",
                table: "OffersTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageFilesTable",
                table: "ImageFilesTable");

            migrationBuilder.DropColumn(
                name: "CertificateFileId",
                table: "OffersTable");

            migrationBuilder.DropColumn(
                name: "OfferFileId",
                table: "OffersTable");

            migrationBuilder.DropColumn(
                name: "PassportFileId",
                table: "OffersTable");

            migrationBuilder.DropColumn(
                name: "PlanFileId",
                table: "OffersTable");

            migrationBuilder.RenameTable(
                name: "ImageFilesTable",
                newName: "DbImageFileModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbImageFileModel",
                table: "DbImageFileModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompaniesTable_DbImageFileModel_LogoFileId",
                table: "CompaniesTable",
                column: "LogoFileId",
                principalTable: "DbImageFileModel",
                principalColumn: "Id");
        }
    }
}
