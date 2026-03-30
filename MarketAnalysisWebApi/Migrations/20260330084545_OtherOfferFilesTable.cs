using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAnalysisWebApi.Migrations
{
    /// <inheritdoc />
    public partial class OtherOfferFilesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OtherOferFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OfferId = table.Column<Guid>(type: "uuid", nullable: false),
                    MyProperty = table.Column<int>(type: "integer", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    ContentType = table.Column<string>(type: "text", nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    FileData = table.Column<byte[]>(type: "bytea", nullable: true),
                    DbBusinessAccountId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherOferFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherOferFiles_BusinessAccounts_DbBusinessAccountId",
                        column: x => x.DbBusinessAccountId,
                        principalTable: "BusinessAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OtherOferFiles_OffersTable_OfferId",
                        column: x => x.OfferId,
                        principalTable: "OffersTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OtherOferFiles_DbBusinessAccountId",
                table: "OtherOferFiles",
                column: "DbBusinessAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherOferFiles_OfferId",
                table: "OtherOferFiles",
                column: "OfferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtherOferFiles");
        }
    }
}
