using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vezeeta.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("27cc8a2c-2d86-4b66-bc04-8a6ec21719f7"), "98052892-9c41-469f-ab73-b6364e1d504f", "Patient", "PATIENT" },
                    { new Guid("9dbb3cda-bd5f-472b-850f-ff78122a97ab"), "d0bacb5c-6d29-420a-a65c-da53d348dbfa", "Admin", "ADMIN" },
                    { new Guid("f910e372-849d-4885-a2d5-93b117071862"), "e912a3ff-3c8c-4246-848d-b4a58ec9c6f2", "Doctor", "DOCTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("27cc8a2c-2d86-4b66-bc04-8a6ec21719f7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9dbb3cda-bd5f-472b-850f-ff78122a97ab"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f910e372-849d-4885-a2d5-93b117071862"));
        }
    }
}
