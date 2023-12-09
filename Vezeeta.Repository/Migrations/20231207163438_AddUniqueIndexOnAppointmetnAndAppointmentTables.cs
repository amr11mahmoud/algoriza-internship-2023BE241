using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexOnAppointmetnAndAppointmentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "AppointmentTimes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "aba34110-5d33-47d7-8d83-538946095bbc");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "792b5e01-420f-475d-983f-f96e0cd1672b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "2e678876-66a3-4695-ad15-a7cbd60c22d4");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentTimes_Time_AppointmentId",
                table: "AppointmentTimes",
                columns: new[] { "Time", "AppointmentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Day_DoctorId",
                table: "Appointments",
                columns: new[] { "Day", "DoctorId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppointmentTimes_Time_AppointmentId",
                table: "AppointmentTimes");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_Day_DoctorId",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "AppointmentTimes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "040411bc-2138-4d98-811b-808ff5f29fcc");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "1e3f642c-d951-4630-8b45-8e0d9715ca5d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "9e5e770d-07f4-4a89-bae9-8d64e9ed6a72");
        }
    }
}
