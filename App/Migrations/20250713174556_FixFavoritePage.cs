using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    /// <inheritdoc />
    public partial class FixFavoritePage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("efdaa81f-9f1e-4e3d-8050-245cca05bf4b"),
                column: "Time",
                value: new DateTime(2025, 7, 13, 17, 45, 56, 541, DateTimeKind.Utc).AddTicks(660));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("efdaa81f-9f1e-4e3d-8050-245cca05bf4b"),
                column: "Time",
                value: new DateTime(2025, 7, 10, 17, 31, 23, 296, DateTimeKind.Utc).AddTicks(7560));
        }
    }
}
