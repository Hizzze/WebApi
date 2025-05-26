using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    /// <inheritdoc />
    public partial class ReloadAppDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("efdaa81f-9f1e-4e3d-8050-245cca05bf4b"),
                column: "Time",
                value: new DateTime(2025, 5, 26, 16, 18, 56, 467, DateTimeKind.Utc).AddTicks(9800));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("efdaa81f-9f1e-4e3d-8050-245cca05bf4b"),
                column: "Time",
                value: new DateTime(2025, 5, 18, 19, 37, 58, 955, DateTimeKind.Utc).AddTicks(2750));
        }
    }
}
