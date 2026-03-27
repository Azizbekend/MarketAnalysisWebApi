using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAnalysisWebApi.Migrations
{
    /// <inheritdoc />
    public partial class pumpConfigAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PumpTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PumpTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbPumpConfiguration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PumpTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PumpedLiquidType = table.Column<int>(type: "integer", nullable: false),
                    PumpEfficiency = table.Column<double>(type: "double precision", nullable: false),
                    LiquidTemperature = table.Column<double>(type: "double precision", nullable: false),
                    MineralParticlesSize = table.Column<double>(type: "double precision", nullable: false),
                    MineralParticlesConcentration = table.Column<double>(type: "double precision", nullable: false),
                    BigParticleExistance = table.Column<bool>(type: "boolean", nullable: false),
                    SpecificWastes = table.Column<string>(type: "text", nullable: true),
                    LiquidDensity = table.Column<double>(type: "double precision", nullable: false),
                    RequiredPressure = table.Column<double>(type: "double precision", nullable: false),
                    RequiredOutPressure = table.Column<double>(type: "double precision", nullable: false),
                    PressureLoses = table.Column<double>(type: "double precision", nullable: false),
                    NetworkLength = table.Column<double>(type: "double precision", nullable: false),
                    PipesConditions = table.Column<int>(type: "integer", nullable: false),
                    PumpDiameter = table.Column<double>(type: "double precision", nullable: false),
                    GeodesicalMarks = table.Column<string>(type: "text", nullable: true),
                    ExplosionProtection = table.Column<bool>(type: "boolean", nullable: false),
                    ControlType = table.Column<string>(type: "text", nullable: true),
                    PowerCurrentType = table.Column<string>(type: "text", nullable: true),
                    WorkPower = table.Column<double>(type: "double precision", nullable: false),
                    FrequencyConverter = table.Column<bool>(type: "boolean", nullable: false),
                    PowerCableLength = table.Column<double>(type: "double precision", nullable: false),
                    LiftingTransportEquipment = table.Column<bool>(type: "boolean", nullable: false),
                    FlushValve = table.Column<bool>(type: "boolean", nullable: false),
                    OtherLevelMeters = table.Column<bool>(type: "boolean", nullable: false),
                    OtherRequirements = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPumpConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbPumpConfiguration_PumpTypes_PumpTypeId",
                        column: x => x.PumpTypeId,
                        principalTable: "PumpTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DryPumps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PumpTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    SuctionHeight = table.Column<double>(type: "double precision", nullable: false),
                    InstalationType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DryPumps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DryPumps_PumpTypes_PumpTypeId",
                        column: x => x.PumpTypeId,
                        principalTable: "PumpTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmersiblePumps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PumpTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PotentialDepth = table.Column<double>(type: "double precision", nullable: false),
                    InstalationType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmersiblePumps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmersiblePumps_PumpTypes_PumpTypeId",
                        column: x => x.PumpTypeId,
                        principalTable: "PumpTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbPumpConfiguration_PumpTypeId",
                table: "DbPumpConfiguration",
                column: "PumpTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DryPumps_PumpTypeId",
                table: "DryPumps",
                column: "PumpTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmersiblePumps_PumpTypeId",
                table: "SubmersiblePumps",
                column: "PumpTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbPumpConfiguration");

            migrationBuilder.DropTable(
                name: "DryPumps");

            migrationBuilder.DropTable(
                name: "SubmersiblePumps");

            migrationBuilder.DropTable(
                name: "PumpTypes");
        }
    }
}
