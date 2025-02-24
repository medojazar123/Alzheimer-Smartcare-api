using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class addedneonconnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02668ce4-9074-4bad-aa10-761eec532a3f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c76a1b7-932d-45e6-8461-41e6e5bae533");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99394194-1360-4f61-b2df-eca8a5baaa53");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8c4935e3-144c-4f7d-9f3d-3b89b5b5d91a", null, "Doctor", "DOCTOR" },
                    { "9a4264d9-591d-46af-8ff9-312c609cd7bb", null, "Patient", "PATIENT" },
                    { "f954865c-a91c-4fb5-8df9-9c32d26f2cf4", null, "Caregiver", "CAREGIVER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c4935e3-144c-4f7d-9f3d-3b89b5b5d91a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a4264d9-591d-46af-8ff9-312c609cd7bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f954865c-a91c-4fb5-8df9-9c32d26f2cf4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "02668ce4-9074-4bad-aa10-761eec532a3f", null, "Doctor", "DOCTOR" },
                    { "0c76a1b7-932d-45e6-8461-41e6e5bae533", null, "Caregiver", "CAREGIVER" },
                    { "99394194-1360-4f61-b2df-eca8a5baaa53", null, "Patient", "PATIENT" }
                });
        }
    }
}
