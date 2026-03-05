using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAnalysisWebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyTypesTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTypesTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationTypesTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConfigTypeName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationTypesTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbImageFileModel",
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
                    table.PrimaryKey("PK_DbImageFileModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersRolesTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRolesTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ConfigTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentTable_ConfigurationTypesTable_ConfigTypeId",
                        column: x => x.ConfigTypeId,
                        principalTable: "ConfigurationTypesTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompaniesTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullCompanyName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortCompanyName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    INN = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    KPP = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    JurAdress = table.Column<string>(type: "text", nullable: true),
                    LogoFileId = table.Column<Guid>(type: "uuid", nullable: true),
                    CompanyTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompaniesTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompaniesTable_CompanyTypesTable_CompanyTypeId",
                        column: x => x.CompanyTypeId,
                        principalTable: "CompanyTypesTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompaniesTable_DbImageFileModel_LogoFileId",
                        column: x => x.LogoFileId,
                        principalTable: "DbImageFileModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsersTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersTable_UsersRolesTable_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UsersRolesTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OffersTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameByProject = table.Column<string>(type: "text", nullable: true),
                    NameBySupplier = table.Column<string>(type: "text", nullable: true),
                    CurrentPriceNDS = table.Column<double>(type: "double precision", nullable: false),
                    WarehouseLocation = table.Column<string>(type: "text", nullable: true),
                    SupplierSiteURL = table.Column<string>(type: "text", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OffersTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OffersTable_CompaniesTable_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "CompaniesTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectRequestsTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameByProjectDocs = table.Column<string>(type: "text", nullable: false),
                    ObjectName = table.Column<string>(type: "text", nullable: false),
                    LocationRegion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CustomerName = table.Column<string>(type: "text", nullable: false),
                    ContactName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConfigTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRequestsTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectRequestsTable_ConfigurationTypesTable_ConfigTypeId",
                        column: x => x.ConfigTypeId,
                        principalTable: "ConfigurationTypesTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectRequestsTable_UsersTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierUsersCompaniesTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierUsersCompaniesTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierUsersCompaniesTable_CompaniesTable_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "CompaniesTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierUsersCompaniesTable_UsersTable_SupplierUserId",
                        column: x => x.SupplierUserId,
                        principalTable: "UsersTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DbKnsConfig",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Perfomance = table.Column<double>(type: "double precision", nullable: false),
                    Units = table.Column<int>(type: "integer", nullable: false),
                    RequiredPumpPressure = table.Column<double>(type: "double precision", nullable: false),
                    ActivePumpsCount = table.Column<int>(type: "integer", nullable: false),
                    ReservePumpsCount = table.Column<int>(type: "integer", nullable: false),
                    PumpsToWarehouseCount = table.Column<int>(type: "integer", nullable: false),
                    PType = table.Column<int>(type: "integer", nullable: false),
                    EnvironmentTemperature = table.Column<int>(type: "integer", nullable: false),
                    ExplosionProtection = table.Column<bool>(type: "boolean", nullable: false),
                    SupplyPipelineDepth = table.Column<int>(type: "integer", nullable: false),
                    SupplyPipelineDiameter = table.Column<int>(type: "integer", nullable: false),
                    SupplyPipelineMaterial = table.Column<int>(type: "integer", nullable: false),
                    SupplyPipelineDirectionInHours = table.Column<int>(type: "integer", nullable: false),
                    PressurePipelineDepth = table.Column<int>(type: "integer", nullable: false),
                    PressurePipelineDiameter = table.Column<int>(type: "integer", nullable: false),
                    PressurePipelineMaterial = table.Column<int>(type: "integer", nullable: false),
                    PressurePipelineDirectionInHours = table.Column<int>(type: "integer", nullable: false),
                    HasManyExitPressurePipelines = table.Column<bool>(type: "boolean", nullable: false),
                    ExpectedDiameterOfPumpStation = table.Column<int>(type: "integer", nullable: false),
                    ExpectedHeightOfPumpStation = table.Column<int>(type: "integer", nullable: false),
                    InsulatedHousingDepth = table.Column<int>(type: "integer", nullable: false),
                    PowerContactsToController = table.Column<int>(type: "integer", nullable: false),
                    Place = table.Column<int>(type: "integer", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbKnsConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbKnsConfig_ProjectRequestsTable_RequestId",
                        column: x => x.RequestId,
                        principalTable: "ProjectRequestsTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipRequestTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipRequestTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipRequestTable_EquipmentTable_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "EquipmentTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipRequestTable_ProjectRequestsTable_RequestId",
                        column: x => x.RequestId,
                        principalTable: "ProjectRequestsTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoritesTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritesTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoritesTable_ProjectRequestsTable_RequestId",
                        column: x => x.RequestId,
                        principalTable: "ProjectRequestsTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoritesTable_UsersTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompaniesTable_CompanyTypeId",
                table: "CompaniesTable",
                column: "CompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompaniesTable_LogoFileId",
                table: "CompaniesTable",
                column: "LogoFileId");

            migrationBuilder.CreateIndex(
                name: "IX_DbKnsConfig_RequestId",
                table: "DbKnsConfig",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTable_ConfigTypeId",
                table: "EquipmentTable",
                column: "ConfigTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipRequestTable_EquipmentId",
                table: "EquipRequestTable",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipRequestTable_RequestId",
                table: "EquipRequestTable",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritesTable_RequestId",
                table: "FavoritesTable",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritesTable_UserId",
                table: "FavoritesTable",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OffersTable_CompanyId",
                table: "OffersTable",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRequestsTable_ConfigTypeId",
                table: "ProjectRequestsTable",
                column: "ConfigTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRequestsTable_UserId",
                table: "ProjectRequestsTable",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierUsersCompaniesTable_CompanyId",
                table: "SupplierUsersCompaniesTable",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierUsersCompaniesTable_SupplierUserId",
                table: "SupplierUsersCompaniesTable",
                column: "SupplierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersTable_RoleId",
                table: "UsersTable",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbKnsConfig");

            migrationBuilder.DropTable(
                name: "EquipRequestTable");

            migrationBuilder.DropTable(
                name: "FavoritesTable");

            migrationBuilder.DropTable(
                name: "OffersTable");

            migrationBuilder.DropTable(
                name: "SupplierUsersCompaniesTable");

            migrationBuilder.DropTable(
                name: "EquipmentTable");

            migrationBuilder.DropTable(
                name: "ProjectRequestsTable");

            migrationBuilder.DropTable(
                name: "CompaniesTable");

            migrationBuilder.DropTable(
                name: "ConfigurationTypesTable");

            migrationBuilder.DropTable(
                name: "UsersTable");

            migrationBuilder.DropTable(
                name: "CompanyTypesTable");

            migrationBuilder.DropTable(
                name: "DbImageFileModel");

            migrationBuilder.DropTable(
                name: "UsersRolesTable");
        }
    }
}
