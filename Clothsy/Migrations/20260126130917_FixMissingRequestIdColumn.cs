using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clothsy.Migrations
{
    /// <inheritdoc />
    public partial class FixMissingRequestIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestId",
                table: "DonationRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 13, 9, 16, 602, DateTimeKind.Utc).AddTicks(7829));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 13, 9, 16, 602, DateTimeKind.Utc).AddTicks(7836));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 13, 9, 16, 602, DateTimeKind.Utc).AddTicks(7839));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 13, 9, 16, 602, DateTimeKind.Utc).AddTicks(7842));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 13, 9, 16, 602, DateTimeKind.Utc).AddTicks(7844));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 13, 9, 16, 602, DateTimeKind.Utc).AddTicks(7846));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 13, 9, 16, 602, DateTimeKind.Utc).AddTicks(7848));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 13, 9, 16, 602, DateTimeKind.Utc).AddTicks(7850));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 13, 9, 16, 602, DateTimeKind.Utc).AddTicks(7853));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 13, 9, 16, 602, DateTimeKind.Utc).AddTicks(7855));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 13, 9, 16, 602, DateTimeKind.Utc).AddTicks(7857));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 13, 9, 16, 602, DateTimeKind.Utc).AddTicks(7859));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 13, 9, 16, 602, DateTimeKind.Utc).AddTicks(7861));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 13, 9, 16, 602, DateTimeKind.Utc).AddTicks(7863));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "DonationRequests");

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 12, 39, 30, 286, DateTimeKind.Utc).AddTicks(621));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 12, 39, 30, 286, DateTimeKind.Utc).AddTicks(631));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 12, 39, 30, 286, DateTimeKind.Utc).AddTicks(635));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 12, 39, 30, 286, DateTimeKind.Utc).AddTicks(641));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 12, 39, 30, 286, DateTimeKind.Utc).AddTicks(643));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 12, 39, 30, 286, DateTimeKind.Utc).AddTicks(648));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 12, 39, 30, 286, DateTimeKind.Utc).AddTicks(650));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 12, 39, 30, 286, DateTimeKind.Utc).AddTicks(652));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 12, 39, 30, 286, DateTimeKind.Utc).AddTicks(655));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 12, 39, 30, 286, DateTimeKind.Utc).AddTicks(657));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 12, 39, 30, 286, DateTimeKind.Utc).AddTicks(659));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 12, 39, 30, 286, DateTimeKind.Utc).AddTicks(661));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 12, 39, 30, 286, DateTimeKind.Utc).AddTicks(663));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 12, 39, 30, 286, DateTimeKind.Utc).AddTicks(665));
        }
    }
}
