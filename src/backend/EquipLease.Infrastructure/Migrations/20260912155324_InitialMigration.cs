using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EquipLease.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessEquipmentTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Area = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessEquipmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionFacilities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StandardAreaForEquipment = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionFacilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentPlacementContracts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionFacilityId = table.Column<long>(type: "bigint", nullable: false),
                    ProcessEquipmentTypeId = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfEquipmentUnits = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentPlacementContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentPlacementContracts_ProcessEquipmentTypes_ProcessEquipmentTypeId",
                        column: x => x.ProcessEquipmentTypeId,
                        principalTable: "ProcessEquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentPlacementContracts_ProductionFacilities_ProductionFacilityId",
                        column: x => x.ProductionFacilityId,
                        principalTable: "ProductionFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProcessEquipmentTypes",
                columns: new[] { "Id", "Area", "Code", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 50.00m, "PET001", new DateTimeOffset(new DateTime(2026, 9, 12, 18, 52, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Equipment Type 1", new DateTimeOffset(new DateTime(2026, 9, 12, 18, 52, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2L, 75.00m, "PET002", new DateTimeOffset(new DateTime(2026, 9, 12, 18, 52, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Equipment Type 2", new DateTimeOffset(new DateTime(2026, 9, 12, 18, 52, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 3L, 100.00m, "PET003", new DateTimeOffset(new DateTime(2026, 9, 12, 18, 52, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Equipment Type 3", new DateTimeOffset(new DateTime(2026, 9, 12, 18, 52, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "ProductionFacilities",
                columns: new[] { "Id", "Code", "CreatedAt", "Name", "StandardAreaForEquipment", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, "PF001", new DateTimeOffset(new DateTime(2026, 9, 12, 18, 52, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Facility 1", 1000.00m, new DateTimeOffset(new DateTime(2026, 9, 12, 18, 52, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2L, "PF002", new DateTimeOffset(new DateTime(2026, 9, 12, 18, 52, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Facility 2", 1500.00m, new DateTimeOffset(new DateTime(2026, 9, 12, 18, 52, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 3L, "PF003", new DateTimeOffset(new DateTime(2026, 9, 12, 18, 52, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Facility 3", 2000.00m, new DateTimeOffset(new DateTime(2026, 9, 12, 18, 52, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPlacementContracts_ProcessEquipmentTypeId",
                table: "EquipmentPlacementContracts",
                column: "ProcessEquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPlacementContracts_ProductionFacilityId",
                table: "EquipmentPlacementContracts",
                column: "ProductionFacilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentPlacementContracts");

            migrationBuilder.DropTable(
                name: "ProcessEquipmentTypes");

            migrationBuilder.DropTable(
                name: "ProductionFacilities");
        }
    }
}
