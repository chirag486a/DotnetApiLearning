using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class seedRole2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49c6c6cf-73a4-4de0-ba6b-3d59b9cc3b9d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53113a54-299f-47e2-bb24-cca01fb313a9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "99c8c403-b3d1-49af-ae81-2df9e312c202", null, "User", "USER" },
                    { "d325b557-0e4b-4c10-a965-d4864c486c4d", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99c8c403-b3d1-49af-ae81-2df9e312c202");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d325b557-0e4b-4c10-a965-d4864c486c4d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "49c6c6cf-73a4-4de0-ba6b-3d59b9cc3b9d", null, "User", "USER" },
                    { "53113a54-299f-47e2-bb24-cca01fb313a9", null, "Admin", "ADMIN" }
                });
        }
    }
}
