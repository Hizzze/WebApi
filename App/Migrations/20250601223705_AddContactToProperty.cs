using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    /// <inheritdoc />
    public partial class AddContactToProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("efdaa81f-9f1e-4e3d-8050-245cca05bf4b"),
                column: "Time",
                value: new DateTime(2025, 6, 1, 22, 37, 5, 19, DateTimeKind.Utc).AddTicks(6650));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ContactPhone",
                table: "Properties");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("efdaa81f-9f1e-4e3d-8050-245cca05bf4b"),
                column: "Time",
                value: new DateTime(2025, 5, 26, 16, 18, 56, 467, DateTimeKind.Utc).AddTicks(9800));
        }
    }
}
