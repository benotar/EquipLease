using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EquipLease.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProcessEquipmentTypes",
                columns: new[] { "Id", "Area", "Code", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 50.00m, "PET001", new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3714), "Equipment Type 1", new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3715) },
                    { 2, 75.00m, "PET002", new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3719), "Equipment Type 2", new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3720) },
                    { 3, 100.00m, "PET003", new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3722), "Equipment Type 3", new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3723) }
                });

            migrationBuilder.InsertData(
                table: "ProductionFacilities",
                columns: new[] { "Id", "Code", "CreatedAt", "Name", "StandardAreaForEquipment", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "PF001", new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3490), "Facility 1", 1000.00m, new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3495) },
                    { 2, "PF002", new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3505), "Facility 2", 1500.00m, new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3506) },
                    { 3, "PF003", new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3509), "Facility 3", 2000.00m, new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3509) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProcessEquipmentTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProcessEquipmentTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProcessEquipmentTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductionFacilities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductionFacilities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductionFacilities",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
