using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class addedValidationsLocal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fb466ce-c688-4570-b37d-fe88fee40dc6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93a5ebbb-2c6e-4b07-a662-80c248d2a28a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bff0bda8-137e-48b9-b90a-15812cbc4f7b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "23ec0f50-21f5-4260-924d-361852893ca1", null, "Doctor", "DOCTOR" },
                    { "68c2fc31-44ea-4400-b0df-7c1c784c04a3", null, "Caregiver", "CAREGIVER" },
                    { "ee6e872b-f11c-4feb-8356-caac439a4dd0", null, "Patient", "PATIENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23ec0f50-21f5-4260-924d-361852893ca1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "68c2fc31-44ea-4400-b0df-7c1c784c04a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee6e872b-f11c-4feb-8356-caac439a4dd0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6fb466ce-c688-4570-b37d-fe88fee40dc6", null, "Caregiver", "CAREGIVER" },
                    { "93a5ebbb-2c6e-4b07-a662-80c248d2a28a", null, "Doctor", "DOCTOR" },
                    { "bff0bda8-137e-48b9-b90a-15812cbc4f7b", null, "Patient", "PATIENT" }
                });
        }
    }
}
