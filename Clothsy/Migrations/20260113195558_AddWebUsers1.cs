using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clothsy.Migrations
{
    /// <inheritdoc />
    public partial class AddWebUsers1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedHub",
                table: "Requests");

            migrationBuilder.AddColumn<string>(
                name: "HubCode",
                table: "WebUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HubId",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 19, 55, 58, 180, DateTimeKind.Utc).AddTicks(951));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 19, 55, 58, 180, DateTimeKind.Utc).AddTicks(956));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 19, 55, 58, 180, DateTimeKind.Utc).AddTicks(958));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 19, 55, 58, 180, DateTimeKind.Utc).AddTicks(961));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 19, 55, 58, 180, DateTimeKind.Utc).AddTicks(962));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 19, 55, 58, 180, DateTimeKind.Utc).AddTicks(964));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 19, 55, 58, 180, DateTimeKind.Utc).AddTicks(966));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 19, 55, 58, 180, DateTimeKind.Utc).AddTicks(968));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 19, 55, 58, 180, DateTimeKind.Utc).AddTicks(969));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 19, 55, 58, 180, DateTimeKind.Utc).AddTicks(971));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 19, 55, 58, 180, DateTimeKind.Utc).AddTicks(973));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 19, 55, 58, 180, DateTimeKind.Utc).AddTicks(974));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 19, 55, 58, 180, DateTimeKind.Utc).AddTicks(976));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 19, 55, 58, 180, DateTimeKind.Utc).AddTicks(977));

            migrationBuilder.CreateIndex(
                name: "IX_Requests_HubId",
                table: "Requests",
                column: "HubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_WebUsers_HubId",
                table: "Requests",
                column: "HubId",
                principalTable: "WebUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_WebUsers_HubId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_HubId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "HubCode",
                table: "WebUsers");

            migrationBuilder.DropColumn(
                name: "HubId",
                table: "Requests");

            migrationBuilder.AddColumn<string>(
                name: "AssignedHub",
                table: "Requests",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 38, 17, 17, DateTimeKind.Utc).AddTicks(442));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 38, 17, 17, DateTimeKind.Utc).AddTicks(446));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 38, 17, 17, DateTimeKind.Utc).AddTicks(448));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 38, 17, 17, DateTimeKind.Utc).AddTicks(449));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 38, 17, 17, DateTimeKind.Utc).AddTicks(451));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 38, 17, 17, DateTimeKind.Utc).AddTicks(452));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 38, 17, 17, DateTimeKind.Utc).AddTicks(454));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 38, 17, 17, DateTimeKind.Utc).AddTicks(455));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 38, 17, 17, DateTimeKind.Utc).AddTicks(456));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 38, 17, 17, DateTimeKind.Utc).AddTicks(459));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 38, 17, 17, DateTimeKind.Utc).AddTicks(460));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 38, 17, 17, DateTimeKind.Utc).AddTicks(462));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 38, 17, 17, DateTimeKind.Utc).AddTicks(463));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 13, 18, 38, 17, 17, DateTimeKind.Utc).AddTicks(464));
        }
    }
}
