using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clothsy.Migrations
{
    /// <inheritdoc />
    public partial class Updatedhub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pincode",
                table: "Hubs",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Pincode" },
                values: new object[] { new DateTime(2026, 1, 25, 11, 35, 25, 443, DateTimeKind.Utc).AddTicks(5110), null });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Pincode" },
                values: new object[] { new DateTime(2026, 1, 25, 11, 35, 25, 443, DateTimeKind.Utc).AddTicks(5121), null });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Pincode" },
                values: new object[] { new DateTime(2026, 1, 25, 11, 35, 25, 443, DateTimeKind.Utc).AddTicks(5126), null });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Pincode" },
                values: new object[] { new DateTime(2026, 1, 25, 11, 35, 25, 443, DateTimeKind.Utc).AddTicks(5130), null });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Pincode" },
                values: new object[] { new DateTime(2026, 1, 25, 11, 35, 25, 443, DateTimeKind.Utc).AddTicks(5134), null });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Pincode" },
                values: new object[] { new DateTime(2026, 1, 25, 11, 35, 25, 443, DateTimeKind.Utc).AddTicks(5137), null });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Pincode" },
                values: new object[] { new DateTime(2026, 1, 25, 11, 35, 25, 443, DateTimeKind.Utc).AddTicks(5140), null });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "Pincode" },
                values: new object[] { new DateTime(2026, 1, 25, 11, 35, 25, 443, DateTimeKind.Utc).AddTicks(5144), null });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "Pincode" },
                values: new object[] { new DateTime(2026, 1, 25, 11, 35, 25, 443, DateTimeKind.Utc).AddTicks(5147), null });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "Pincode" },
                values: new object[] { new DateTime(2026, 1, 25, 11, 35, 25, 443, DateTimeKind.Utc).AddTicks(5150), null });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "Pincode" },
                values: new object[] { new DateTime(2026, 1, 25, 11, 35, 25, 443, DateTimeKind.Utc).AddTicks(5153), null });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "Pincode" },
                values: new object[] { new DateTime(2026, 1, 25, 11, 35, 25, 443, DateTimeKind.Utc).AddTicks(5157), null });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "Pincode" },
                values: new object[] { new DateTime(2026, 1, 25, 11, 35, 25, 443, DateTimeKind.Utc).AddTicks(5160), null });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "Pincode" },
                values: new object[] { new DateTime(2026, 1, 25, 11, 35, 25, 443, DateTimeKind.Utc).AddTicks(5163), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pincode",
                table: "Hubs");

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 55, 59, 197, DateTimeKind.Utc).AddTicks(7593));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 55, 59, 197, DateTimeKind.Utc).AddTicks(7607));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 55, 59, 197, DateTimeKind.Utc).AddTicks(7611));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 55, 59, 197, DateTimeKind.Utc).AddTicks(7656));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 55, 59, 197, DateTimeKind.Utc).AddTicks(7659));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 55, 59, 197, DateTimeKind.Utc).AddTicks(7661));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 55, 59, 197, DateTimeKind.Utc).AddTicks(7664));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 55, 59, 197, DateTimeKind.Utc).AddTicks(7666));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 55, 59, 197, DateTimeKind.Utc).AddTicks(7669));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 55, 59, 197, DateTimeKind.Utc).AddTicks(7671));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 55, 59, 197, DateTimeKind.Utc).AddTicks(7674));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 55, 59, 197, DateTimeKind.Utc).AddTicks(7680));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 55, 59, 197, DateTimeKind.Utc).AddTicks(7683));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 10, 55, 59, 197, DateTimeKind.Utc).AddTicks(7685));
        }
    }
}
