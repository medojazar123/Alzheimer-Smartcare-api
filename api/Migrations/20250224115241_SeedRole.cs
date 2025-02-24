using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SeedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userType",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b8da2b1-40a1-47aa-979b-c5a3e9623fb8", null, "Patient", "PATIENT" },
                    { "b8232444-63b4-4db1-99ab-d0108bdd7b59", null, "Doctor", "DOCTOR" },
                    { "dc622ecc-06b1-4567-86b3-fe96e7511777", null, "Caregiver", "CAREGIVER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b8da2b1-40a1-47aa-979b-c5a3e9623fb8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8232444-63b4-4db1-99ab-d0108bdd7b59");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc622ecc-06b1-4567-86b3-fe96e7511777");

            migrationBuilder.DropColumn(
                name: "userType",
                table: "AspNetUsers");
        }
    }
}
