using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddFaceImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5da9897d-c78c-4a90-83ca-e1261f904374");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3f8059a-3a04-4f65-a3e0-5ff7a79e7673");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c33d72b1-500c-42c5-9ffd-ee1e9b3b0344");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5da9897d-c78c-4a90-83ca-e1261f904374", null, "Doctor", "DOCTOR" },
                    { "b3f8059a-3a04-4f65-a3e0-5ff7a79e7673", null, "Caregiver", "CAREGIVER" },
                    { "c33d72b1-500c-42c5-9ffd-ee1e9b3b0344", null, "Patient", "PATIENT" }
                });
        }
    }
}
