using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clothsy.Migrations
{
    /// <inheritdoc />
    public partial class workinghour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkingHours",
                table: "Hubs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 11, 13, 6, 59, 137, DateTimeKind.Utc).AddTicks(2144), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 11, 13, 6, 59, 137, DateTimeKind.Utc).AddTicks(2148), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 11, 13, 6, 59, 137, DateTimeKind.Utc).AddTicks(2150), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 11, 13, 6, 59, 137, DateTimeKind.Utc).AddTicks(2152), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 11, 13, 6, 59, 137, DateTimeKind.Utc).AddTicks(2154), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 11, 13, 6, 59, 137, DateTimeKind.Utc).AddTicks(2155), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 11, 13, 6, 59, 137, DateTimeKind.Utc).AddTicks(2156), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 11, 13, 6, 59, 137, DateTimeKind.Utc).AddTicks(2158), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 11, 13, 6, 59, 137, DateTimeKind.Utc).AddTicks(2159), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 11, 13, 6, 59, 137, DateTimeKind.Utc).AddTicks(2160), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 11, 13, 6, 59, 137, DateTimeKind.Utc).AddTicks(2162), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 11, 13, 6, 59, 137, DateTimeKind.Utc).AddTicks(2163), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 11, 13, 6, 59, 137, DateTimeKind.Utc).AddTicks(2164), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 11, 13, 6, 59, 137, DateTimeKind.Utc).AddTicks(2165), "10:00 AM - 5:00 PM" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkingHours",
                table: "Hubs");

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 11, 54, 6, 287, DateTimeKind.Utc).AddTicks(7447));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 11, 54, 6, 287, DateTimeKind.Utc).AddTicks(7453));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 11, 54, 6, 287, DateTimeKind.Utc).AddTicks(7455));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 11, 54, 6, 287, DateTimeKind.Utc).AddTicks(7456));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 11, 54, 6, 287, DateTimeKind.Utc).AddTicks(7458));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 11, 54, 6, 287, DateTimeKind.Utc).AddTicks(7459));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 11, 54, 6, 287, DateTimeKind.Utc).AddTicks(7460));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 11, 54, 6, 287, DateTimeKind.Utc).AddTicks(7462));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 11, 54, 6, 287, DateTimeKind.Utc).AddTicks(7463));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 11, 54, 6, 287, DateTimeKind.Utc).AddTicks(7464));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 11, 54, 6, 287, DateTimeKind.Utc).AddTicks(7466));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 11, 54, 6, 287, DateTimeKind.Utc).AddTicks(7467));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 11, 54, 6, 287, DateTimeKind.Utc).AddTicks(7468));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 11, 11, 54, 6, 287, DateTimeKind.Utc).AddTicks(7469));
        }
    }
}
