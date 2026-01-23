using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clothsy.Migrations
{
    /// <inheritdoc />
    public partial class SplitWorkingHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkingHours",
                table: "Hubs",
                newName: "WorkingDays");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CloseTime",
                table: "Hubs",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpenTime",
                table: "Hubs",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CloseTime", "CreatedAt", "OpenTime", "WorkingDays" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new DateTime(2026, 1, 20, 18, 41, 1, 798, DateTimeKind.Utc).AddTicks(5815), new TimeSpan(0, 0, 0, 0, 0), "" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CloseTime", "CreatedAt", "OpenTime", "WorkingDays" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new DateTime(2026, 1, 20, 18, 41, 1, 798, DateTimeKind.Utc).AddTicks(5821), new TimeSpan(0, 0, 0, 0, 0), "" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CloseTime", "CreatedAt", "OpenTime", "WorkingDays" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new DateTime(2026, 1, 20, 18, 41, 1, 798, DateTimeKind.Utc).AddTicks(5822), new TimeSpan(0, 0, 0, 0, 0), "" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CloseTime", "CreatedAt", "OpenTime", "WorkingDays" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new DateTime(2026, 1, 20, 18, 41, 1, 798, DateTimeKind.Utc).AddTicks(5824), new TimeSpan(0, 0, 0, 0, 0), "" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CloseTime", "CreatedAt", "OpenTime", "WorkingDays" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new DateTime(2026, 1, 20, 18, 41, 1, 798, DateTimeKind.Utc).AddTicks(5825), new TimeSpan(0, 0, 0, 0, 0), "" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CloseTime", "CreatedAt", "OpenTime", "WorkingDays" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new DateTime(2026, 1, 20, 18, 41, 1, 798, DateTimeKind.Utc).AddTicks(5827), new TimeSpan(0, 0, 0, 0, 0), "" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CloseTime", "CreatedAt", "OpenTime", "WorkingDays" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new DateTime(2026, 1, 20, 18, 41, 1, 798, DateTimeKind.Utc).AddTicks(5828), new TimeSpan(0, 0, 0, 0, 0), "" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CloseTime", "CreatedAt", "OpenTime", "WorkingDays" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new DateTime(2026, 1, 20, 18, 41, 1, 798, DateTimeKind.Utc).AddTicks(5829), new TimeSpan(0, 0, 0, 0, 0), "" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CloseTime", "CreatedAt", "OpenTime", "WorkingDays" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new DateTime(2026, 1, 20, 18, 41, 1, 798, DateTimeKind.Utc).AddTicks(5831), new TimeSpan(0, 0, 0, 0, 0), "" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CloseTime", "CreatedAt", "OpenTime", "WorkingDays" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new DateTime(2026, 1, 20, 18, 41, 1, 798, DateTimeKind.Utc).AddTicks(5832), new TimeSpan(0, 0, 0, 0, 0), "" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CloseTime", "CreatedAt", "OpenTime", "WorkingDays" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new DateTime(2026, 1, 20, 18, 41, 1, 798, DateTimeKind.Utc).AddTicks(5835), new TimeSpan(0, 0, 0, 0, 0), "" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CloseTime", "CreatedAt", "OpenTime", "WorkingDays" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new DateTime(2026, 1, 20, 18, 41, 1, 798, DateTimeKind.Utc).AddTicks(5836), new TimeSpan(0, 0, 0, 0, 0), "" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CloseTime", "CreatedAt", "OpenTime", "WorkingDays" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new DateTime(2026, 1, 20, 18, 41, 1, 798, DateTimeKind.Utc).AddTicks(5837), new TimeSpan(0, 0, 0, 0, 0), "" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CloseTime", "CreatedAt", "OpenTime", "WorkingDays" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new DateTime(2026, 1, 20, 18, 41, 1, 798, DateTimeKind.Utc).AddTicks(5838), new TimeSpan(0, 0, 0, 0, 0), "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseTime",
                table: "Hubs");

            migrationBuilder.DropColumn(
                name: "OpenTime",
                table: "Hubs");

            migrationBuilder.RenameColumn(
                name: "WorkingDays",
                table: "Hubs",
                newName: "WorkingHours");

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 19, 11, 32, 58, 539, DateTimeKind.Utc).AddTicks(4636), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 19, 11, 32, 58, 539, DateTimeKind.Utc).AddTicks(4641), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 19, 11, 32, 58, 539, DateTimeKind.Utc).AddTicks(4643), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 19, 11, 32, 58, 539, DateTimeKind.Utc).AddTicks(4644), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 19, 11, 32, 58, 539, DateTimeKind.Utc).AddTicks(4646), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 19, 11, 32, 58, 539, DateTimeKind.Utc).AddTicks(4650), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 19, 11, 32, 58, 539, DateTimeKind.Utc).AddTicks(4651), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 19, 11, 32, 58, 539, DateTimeKind.Utc).AddTicks(4653), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 19, 11, 32, 58, 539, DateTimeKind.Utc).AddTicks(4654), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 19, 11, 32, 58, 539, DateTimeKind.Utc).AddTicks(4655), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 19, 11, 32, 58, 539, DateTimeKind.Utc).AddTicks(4657), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 19, 11, 32, 58, 539, DateTimeKind.Utc).AddTicks(4658), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 19, 11, 32, 58, 539, DateTimeKind.Utc).AddTicks(4659), "10:00 AM - 5:00 PM" });

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "WorkingHours" },
                values: new object[] { new DateTime(2026, 1, 19, 11, 32, 58, 539, DateTimeKind.Utc).AddTicks(4661), "10:00 AM - 5:00 PM" });
        }
    }
}
