using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class addedValidations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

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
    }
}
