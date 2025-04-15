using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    /// <inheritdoc />
    public partial class SeedOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a61cd69-c079-46c1-987f-5da06329b874"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "LastName", "Login", "Name", "PasswordHash", "Role", "Time" },
                values: new object[] { new Guid("efdaa81f-9f1e-4e3d-8050-245cca05bf4b"), "Syzov", "admin", "Vlad", "$2a$11$2G3iM7uLfkhhYIFuqEAlA.fLSX8oHpwCz00IBL747VLZvLYAqqcCO", "Owner", new DateTime(2025, 4, 15, 13, 36, 44, 988, DateTimeKind.Utc).AddTicks(7990) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("efdaa81f-9f1e-4e3d-8050-245cca05bf4b"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "LastName", "Login", "Name", "PasswordHash", "Role", "Time" },
                values: new object[] { new Guid("8a61cd69-c079-46c1-987f-5da06329b874"), "Syzov", "admin", "Vlad", "$2a$11$2G3iM7uLfkhhYIFuqEAlA.fLSX8oHpwCz00IBL747VLZvLYAqqcCO", "Owner", new DateTime(2025, 4, 15, 13, 31, 49, 445, DateTimeKind.Utc).AddTicks(9340) });
        }
    }
}
