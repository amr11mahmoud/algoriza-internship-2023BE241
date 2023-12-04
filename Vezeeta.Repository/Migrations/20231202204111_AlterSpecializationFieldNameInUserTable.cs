using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AlterSpecializationFieldNameInUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Specializations_Specialize",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Specialize",
                table: "Users",
                newName: "SpecializationId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Specialize",
                table: "Users",
                newName: "IX_Users_SpecializationId");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d2c8663e-d2bd-45d4-8ace-c19d0eee56cd");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b4ec1217-d033-4e46-be7a-33515f532543");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "bf0b1ac7-1b55-4ce4-ba3b-677261919587");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Specializations_SpecializationId",
                table: "Users",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Specializations_SpecializationId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "SpecializationId",
                table: "Users",
                newName: "Specialize");

            migrationBuilder.RenameIndex(
                name: "IX_Users_SpecializationId",
                table: "Users",
                newName: "IX_Users_Specialize");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4859d505-88a4-4c3b-8c37-e05e6b9fca6c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "81108e30-b978-4436-9945-7185b03d8244");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e658b185-f1f4-456d-a037-2a25bed1ea2a");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Specializations_Specialize",
                table: "Users",
                column: "Specialize",
                principalTable: "Specializations",
                principalColumn: "Id");
        }
    }
}
