using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class addedFaceImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "FaceImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Base64Image = table.Column<string>(type: "text", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaceImages", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaceImages");

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
                    { "23ec0f50-21f5-4260-924d-361852893ca1", null, "Doctor", "DOCTOR" },
                    { "68c2fc31-44ea-4400-b0df-7c1c784c04a3", null, "Caregiver", "CAREGIVER" },
                    { "ee6e872b-f11c-4feb-8356-caac439a4dd0", null, "Patient", "PATIENT" }
                });
        }
    }
}
