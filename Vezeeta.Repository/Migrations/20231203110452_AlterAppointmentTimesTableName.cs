using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AlterAppointmentTimesTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AppointmentSchedules_AppointmentScheduleId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "AppointmentSchedules");

            migrationBuilder.RenameColumn(
                name: "AppointmentScheduleId",
                table: "Bookings",
                newName: "AppointmentTimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_AppointmentScheduleId",
                table: "Bookings",
                newName: "IX_Bookings_AppointmentTimeId");

            migrationBuilder.CreateTable(
                name: "AppointmentTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Booked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentTimes_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "23cd937d-db3b-4d8a-9981-c3dcb334159a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "87bd39c8-17ed-4774-8f2e-1b488d82557a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "f43ece24-2f82-4ddf-94b1-8c173722f755");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentTimes_AppointmentId",
                table: "AppointmentTimes",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AppointmentTimes_AppointmentTimeId",
                table: "Bookings",
                column: "AppointmentTimeId",
                principalTable: "AppointmentTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AppointmentTimes_AppointmentTimeId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "AppointmentTimes");

            migrationBuilder.RenameColumn(
                name: "AppointmentTimeId",
                table: "Bookings",
                newName: "AppointmentScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_AppointmentTimeId",
                table: "Bookings",
                newName: "IX_Bookings_AppointmentScheduleId");

            migrationBuilder.CreateTable(
                name: "AppointmentSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    Booked = table.Column<bool>(type: "bit", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentSchedules_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentSchedules_AppointmentId",
                table: "AppointmentSchedules",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AppointmentSchedules_AppointmentScheduleId",
                table: "Bookings",
                column: "AppointmentScheduleId",
                principalTable: "AppointmentSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
