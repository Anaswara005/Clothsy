using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clothsy.Migrations
{
    /// <inheritdoc />
    public partial class SupportTicketReplies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "SupportTickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Hubs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DistrictId" },
                values: new object[] { new DateTime(2026, 1, 23, 4, 37, 10, 633, DateTimeKind.Utc).AddTicks(7859), 0 });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DistrictId" },
                values: new object[] { new DateTime(2026, 1, 23, 4, 37, 10, 633, DateTimeKind.Utc).AddTicks(7864), 0 });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "DistrictId" },
                values: new object[] { new DateTime(2026, 1, 23, 4, 37, 10, 633, DateTimeKind.Utc).AddTicks(7866), 0 });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "DistrictId" },
                values: new object[] { new DateTime(2026, 1, 23, 4, 37, 10, 633, DateTimeKind.Utc).AddTicks(7868), 0 });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "DistrictId" },
                values: new object[] { new DateTime(2026, 1, 23, 4, 37, 10, 633, DateTimeKind.Utc).AddTicks(7869), 0 });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "DistrictId" },
                values: new object[] { new DateTime(2026, 1, 23, 4, 37, 10, 633, DateTimeKind.Utc).AddTicks(7871), 0 });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "DistrictId" },
                values: new object[] { new DateTime(2026, 1, 23, 4, 37, 10, 633, DateTimeKind.Utc).AddTicks(7872), 0 });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "DistrictId" },
                values: new object[] { new DateTime(2026, 1, 23, 4, 37, 10, 633, DateTimeKind.Utc).AddTicks(7874), 0 });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "DistrictId" },
                values: new object[] { new DateTime(2026, 1, 23, 4, 37, 10, 633, DateTimeKind.Utc).AddTicks(7875), 0 });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "DistrictId" },
                values: new object[] { new DateTime(2026, 1, 23, 4, 37, 10, 633, DateTimeKind.Utc).AddTicks(7876), 0 });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "DistrictId" },
                values: new object[] { new DateTime(2026, 1, 23, 4, 37, 10, 633, DateTimeKind.Utc).AddTicks(7879), 0 });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "DistrictId" },
                values: new object[] { new DateTime(2026, 1, 23, 4, 37, 10, 633, DateTimeKind.Utc).AddTicks(7880), 0 });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "DistrictId" },
                values: new object[] { new DateTime(2026, 1, 23, 4, 37, 10, 633, DateTimeKind.Utc).AddTicks(7881), 0 });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "DistrictId" },
                values: new object[] { new DateTime(2026, 1, 23, 4, 37, 10, 633, DateTimeKind.Utc).AddTicks(7882), 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "SupportTickets");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Hubs");

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 18, 36, 25, 152, DateTimeKind.Utc).AddTicks(6168));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 18, 36, 25, 152, DateTimeKind.Utc).AddTicks(6174));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 18, 36, 25, 152, DateTimeKind.Utc).AddTicks(6176));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 18, 36, 25, 152, DateTimeKind.Utc).AddTicks(6178));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 18, 36, 25, 152, DateTimeKind.Utc).AddTicks(6180));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 18, 36, 25, 152, DateTimeKind.Utc).AddTicks(6182));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 18, 36, 25, 152, DateTimeKind.Utc).AddTicks(6184));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 18, 36, 25, 152, DateTimeKind.Utc).AddTicks(6185));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 18, 36, 25, 152, DateTimeKind.Utc).AddTicks(6187));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 18, 36, 25, 152, DateTimeKind.Utc).AddTicks(6188));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 18, 36, 25, 152, DateTimeKind.Utc).AddTicks(6190));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 18, 36, 25, 152, DateTimeKind.Utc).AddTicks(6191));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 18, 36, 25, 152, DateTimeKind.Utc).AddTicks(6194));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 18, 36, 25, 152, DateTimeKind.Utc).AddTicks(6196));
        }
    }
}
