using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class addedReminders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b6a1116-d715-466d-ae7e-a683cc9b6703");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afb45224-0c94-4b60-93bf-b4e0807ad14d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8482ff9-9c61-4419-b2b4-1d673cf0b201");

            migrationBuilder.CreateTable(
                name: "MedicineReminders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    ScheduledTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Repeat = table.Column<int>(type: "integer", nullable: false),
                    FcmToken = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineReminders", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "50dd27cf-bf98-4588-9316-344f4620ffc3", null, "Patient", "PATIENT" },
                    { "55b0ae73-f320-4bfb-bb66-bd1a4d059cb3", null, "Doctor", "DOCTOR" },
                    { "f310fcff-057c-4bd1-b9f5-eb09037453bd", null, "Caregiver", "CAREGIVER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicineReminders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50dd27cf-bf98-4588-9316-344f4620ffc3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55b0ae73-f320-4bfb-bb66-bd1a4d059cb3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f310fcff-057c-4bd1-b9f5-eb09037453bd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b6a1116-d715-466d-ae7e-a683cc9b6703", null, "Patient", "PATIENT" },
                    { "afb45224-0c94-4b60-93bf-b4e0807ad14d", null, "Doctor", "DOCTOR" },
                    { "d8482ff9-9c61-4419-b2b4-1d673cf0b201", null, "Caregiver", "CAREGIVER" }
                });
        }
    }
}
