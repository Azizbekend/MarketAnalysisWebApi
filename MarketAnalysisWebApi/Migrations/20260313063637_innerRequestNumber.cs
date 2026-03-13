using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAnalysisWebApi.Migrations
{
    /// <inheritdoc />
    public partial class innerRequestNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InnerId",
                table: "ProjectRequestsTable",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InnerId",
                table: "ProjectRequestsTable");
        }
    }
}
