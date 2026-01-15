using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clothsy.Migrations
{
    /// <inheritdoc />
    public partial class Aaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Donations_DonationId",
                table: "Donations");

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 12, 55, 16, 793, DateTimeKind.Utc).AddTicks(527));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 12, 55, 16, 793, DateTimeKind.Utc).AddTicks(538));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 12, 55, 16, 793, DateTimeKind.Utc).AddTicks(543));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 12, 55, 16, 793, DateTimeKind.Utc).AddTicks(547));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 12, 55, 16, 793, DateTimeKind.Utc).AddTicks(550));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 12, 55, 16, 793, DateTimeKind.Utc).AddTicks(554));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 12, 55, 16, 793, DateTimeKind.Utc).AddTicks(557));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 12, 55, 16, 793, DateTimeKind.Utc).AddTicks(560));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 12, 55, 16, 793, DateTimeKind.Utc).AddTicks(565));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 12, 55, 16, 793, DateTimeKind.Utc).AddTicks(568));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 12, 55, 16, 793, DateTimeKind.Utc).AddTicks(571));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 12, 55, 16, 793, DateTimeKind.Utc).AddTicks(574));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 12, 55, 16, 793, DateTimeKind.Utc).AddTicks(577));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 12, 55, 16, 793, DateTimeKind.Utc).AddTicks(580));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 3, 9, 39, 40, 558, DateTimeKind.Utc).AddTicks(5301));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 3, 9, 39, 40, 558, DateTimeKind.Utc).AddTicks(5310));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 3, 9, 39, 40, 558, DateTimeKind.Utc).AddTicks(5314));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 3, 9, 39, 40, 558, DateTimeKind.Utc).AddTicks(5318));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 3, 9, 39, 40, 558, DateTimeKind.Utc).AddTicks(5321));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 3, 9, 39, 40, 558, DateTimeKind.Utc).AddTicks(5324));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 3, 9, 39, 40, 558, DateTimeKind.Utc).AddTicks(5327));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 3, 9, 39, 40, 558, DateTimeKind.Utc).AddTicks(5329));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 3, 9, 39, 40, 558, DateTimeKind.Utc).AddTicks(5332));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 3, 9, 39, 40, 558, DateTimeKind.Utc).AddTicks(5335));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 3, 9, 39, 40, 558, DateTimeKind.Utc).AddTicks(5338));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 3, 9, 39, 40, 558, DateTimeKind.Utc).AddTicks(5340));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 3, 9, 39, 40, 558, DateTimeKind.Utc).AddTicks(5443));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 3, 9, 39, 40, 558, DateTimeKind.Utc).AddTicks(5446));

            migrationBuilder.CreateIndex(
                name: "IX_Donations_DonationId",
                table: "Donations",
                column: "DonationId",
                unique: true);
        }
    }
}
