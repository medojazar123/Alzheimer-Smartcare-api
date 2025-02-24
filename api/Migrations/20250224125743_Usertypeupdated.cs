using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class Usertypeupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "userType",
                table: "AspNetUsers",
                newName: "UserType");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "AspNetUsers",
                newName: "userType");

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
    }
}
