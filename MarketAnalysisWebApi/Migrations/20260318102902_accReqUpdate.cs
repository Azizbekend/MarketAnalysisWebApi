using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAnalysisWebApi.Migrations
{
    /// <inheritdoc />
    public partial class accReqUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbAccountRequest_BusinessAccounts_AccountId",
                table: "DbAccountRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_DbAccountRequest_ProjectRequestsTable_RequestId",
                table: "DbAccountRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbAccountRequest",
                table: "DbAccountRequest");

            migrationBuilder.RenameTable(
                name: "DbAccountRequest",
                newName: "AccountRequests");

            migrationBuilder.RenameIndex(
                name: "IX_DbAccountRequest_RequestId",
                table: "AccountRequests",
                newName: "IX_AccountRequests_RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_DbAccountRequest_AccountId",
                table: "AccountRequests",
                newName: "IX_AccountRequests_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRequests",
                table: "AccountRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRequests_BusinessAccounts_AccountId",
                table: "AccountRequests",
                column: "AccountId",
                principalTable: "BusinessAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRequests_ProjectRequestsTable_RequestId",
                table: "AccountRequests",
                column: "RequestId",
                principalTable: "ProjectRequestsTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRequests_BusinessAccounts_AccountId",
                table: "AccountRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRequests_ProjectRequestsTable_RequestId",
                table: "AccountRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRequests",
                table: "AccountRequests");

            migrationBuilder.RenameTable(
                name: "AccountRequests",
                newName: "DbAccountRequest");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRequests_RequestId",
                table: "DbAccountRequest",
                newName: "IX_DbAccountRequest_RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRequests_AccountId",
                table: "DbAccountRequest",
                newName: "IX_DbAccountRequest_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbAccountRequest",
                table: "DbAccountRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DbAccountRequest_BusinessAccounts_AccountId",
                table: "DbAccountRequest",
                column: "AccountId",
                principalTable: "BusinessAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DbAccountRequest_ProjectRequestsTable_RequestId",
                table: "DbAccountRequest",
                column: "RequestId",
                principalTable: "ProjectRequestsTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
