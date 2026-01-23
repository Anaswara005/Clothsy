using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clothsy.Migrations
{
    /// <inheritdoc />
    public partial class FixDonationRequestCollectedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DonationRequests_DonationId1",
                table: "DonationRequests");

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 38, 38, 255, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 38, 38, 255, DateTimeKind.Utc).AddTicks(4128));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 38, 38, 255, DateTimeKind.Utc).AddTicks(4130));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 38, 38, 255, DateTimeKind.Utc).AddTicks(4132));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 38, 38, 255, DateTimeKind.Utc).AddTicks(4133));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 38, 38, 255, DateTimeKind.Utc).AddTicks(4135));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 38, 38, 255, DateTimeKind.Utc).AddTicks(4136));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 38, 38, 255, DateTimeKind.Utc).AddTicks(4138));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 38, 38, 255, DateTimeKind.Utc).AddTicks(4139));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 38, 38, 255, DateTimeKind.Utc).AddTicks(4140));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 38, 38, 255, DateTimeKind.Utc).AddTicks(4142));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 38, 38, 255, DateTimeKind.Utc).AddTicks(4143));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 38, 38, 255, DateTimeKind.Utc).AddTicks(4144));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 38, 38, 255, DateTimeKind.Utc).AddTicks(4145));

            migrationBuilder.CreateIndex(
                name: "IX_DonationRequests_DonationId1",
                table: "DonationRequests",
                column: "DonationId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DonationRequests_DonationId1",
                table: "DonationRequests");

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 31, 59, 317, DateTimeKind.Utc).AddTicks(9356));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 31, 59, 317, DateTimeKind.Utc).AddTicks(9362));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 31, 59, 317, DateTimeKind.Utc).AddTicks(9364));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 31, 59, 317, DateTimeKind.Utc).AddTicks(9366));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 31, 59, 317, DateTimeKind.Utc).AddTicks(9367));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 31, 59, 317, DateTimeKind.Utc).AddTicks(9369));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 31, 59, 317, DateTimeKind.Utc).AddTicks(9370));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 31, 59, 317, DateTimeKind.Utc).AddTicks(9372));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 31, 59, 317, DateTimeKind.Utc).AddTicks(9373));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 31, 59, 317, DateTimeKind.Utc).AddTicks(9374));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 31, 59, 317, DateTimeKind.Utc).AddTicks(9376));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 31, 59, 317, DateTimeKind.Utc).AddTicks(9377));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 31, 59, 317, DateTimeKind.Utc).AddTicks(9379));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 8, 31, 59, 317, DateTimeKind.Utc).AddTicks(9381));

            migrationBuilder.CreateIndex(
                name: "IX_DonationRequests_DonationId1",
                table: "DonationRequests",
                column: "DonationId1",
                unique: true,
                filter: "[DonationId1] IS NOT NULL");
        }
    }
}
