using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clothsy.Migrations
{
    /// <inheritdoc />
    public partial class Clothsy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonationId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DonorUserId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClothingType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Size = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AssignedHubId = table.Column<int>(type: "int", nullable: false),
                    Photo1Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Photo2Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Photo3Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donations_Hubs_AssignedHubId",
                        column: x => x.AssignedHubId,
                        principalTable: "Hubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donations_Users_DonorUserId",
                        column: x => x.DonorUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Hubs",
                columns: new[] { "Id", "Address", "CreatedAt", "District", "Email", "IsActive", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, " Trivandrum, Kerala", new DateTime(2025, 11, 20, 10, 37, 34, 364, DateTimeKind.Utc).AddTicks(3555), "Trivandrum", "clothsy.trivandrum@clothsy.in", true, "Clothsy Central Hub - Trivandrum", "+91 90000 00000" },
                    { 2, "Kollam, Kerala", new DateTime(2025, 11, 20, 10, 37, 34, 364, DateTimeKind.Utc).AddTicks(3559), "Kollam", "clothsy.kollam@clothsy.in", true, "Clothsy Central Hub - Kollam", "+91 90000 00001" },
                    { 3, "Pathanamthitta, Kerala", new DateTime(2025, 11, 20, 10, 37, 34, 364, DateTimeKind.Utc).AddTicks(3560), "Pathanamthitta", "clothsy.pathanamthitta@clothsy.in", true, "Clothsy Central Hub - Pathanamthitta", "+91 90000 00002" },
                    { 4, "Alappuzha, Kerala", new DateTime(2025, 11, 20, 10, 37, 34, 364, DateTimeKind.Utc).AddTicks(3562), "Alappuzha", "clothsy.alappuzha@clothsy.in", true, "Clothsy Central Hub - Alappuzha", "+91 90000 00003" },
                    { 5, "Kottayam, Kerala", new DateTime(2025, 11, 20, 10, 37, 34, 364, DateTimeKind.Utc).AddTicks(3563), "Kottayam", "clothsy.kottayam@clothsy.in", true, "Clothsy Central Hub - Kottayam", "+91 90000 00004" },
                    { 6, "Idukki, Kerala", new DateTime(2025, 11, 20, 10, 37, 34, 364, DateTimeKind.Utc).AddTicks(3565), "Idukki", "clothsy.idukki@clothsy.in", true, "Clothsy Central Hub - Idukki", "+91 90000 00005" },
                    { 7, "Ernakulam, Kerala", new DateTime(2025, 11, 20, 10, 37, 34, 364, DateTimeKind.Utc).AddTicks(3566), "Ernakulam", "clothsy.ernakulam@clothsy.in", true, "Clothsy Central Hub - Ernakulam", "+91 90000 00006" },
                    { 8, "Thrissur, Kerala", new DateTime(2025, 11, 20, 10, 37, 34, 364, DateTimeKind.Utc).AddTicks(3568), "Thrissur", "clothsy.thrissur@clothsy.in", true, "Clothsy Central Hub - Thrissur", "+91 90000 00007" },
                    { 9, "Palakkad, Kerala", new DateTime(2025, 11, 20, 10, 37, 34, 364, DateTimeKind.Utc).AddTicks(3569), "Palakkad", "clothsy.palakkad@clothsy.in", true, "Clothsy Central Hub - Palakkad", "+91 90000 00008" },
                    { 10, "Malappuram, Kerala", new DateTime(2025, 11, 20, 10, 37, 34, 364, DateTimeKind.Utc).AddTicks(3571), "Malappuram", "clothsy.malappuram@clothsy.in", true, "Clothsy Central Hub - Malappuram", "+91 90000 00009" },
                    { 11, "Kozhikode, Kerala", new DateTime(2025, 11, 20, 10, 37, 34, 364, DateTimeKind.Utc).AddTicks(3572), "Kozhikode", "clothsy.kozhikode@clothsy.in", true, "Clothsy Central Hub - Kozhikode", "+91 90000 00010" },
                    { 12, "Wayanad, Kerala", new DateTime(2025, 11, 20, 10, 37, 34, 364, DateTimeKind.Utc).AddTicks(3574), "Wayanad", "clothsy.wayanad@clothsy.in", true, "Clothsy Central Hub - Wayanad", "+91 90000 00011" },
                    { 13, "Kannur, Kerala", new DateTime(2025, 11, 20, 10, 37, 34, 364, DateTimeKind.Utc).AddTicks(3575), "Kannur", "clothsy.kannur@clothsy.in", true, "Clothsy Central Hub - Kannur", "+91 90000 00012" },
                    { 14, "Kasaragod, Kerala", new DateTime(2025, 11, 20, 10, 37, 34, 364, DateTimeKind.Utc).AddTicks(3577), "Kasaragod", "clothsy.kasaragod@clothsy.in", true, "Clothsy Central Hub - Kasaragod", "+91 90000 00013" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_AssignedHubId",
                table: "Donations",
                column: "AssignedHubId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_DonationId",
                table: "Donations",
                column: "DonationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_DonorUserId",
                table: "Donations",
                column: "DonorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Hubs_Email",
                table: "Hubs",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "Hubs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
