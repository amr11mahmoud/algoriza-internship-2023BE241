using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vezeeta.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataRolesAndSpecializations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "4859d505-88a4-4c3b-8c37-e05e6b9fca6c", "Admin", "ADMIN" },
                    { 2, "81108e30-b978-4436-9945-7185b03d8244", "Doctor", "DOCTOR" },
                    { 3, "e658b185-f1f4-456d-a037-2a25bed1ea2a", "Patient", "PATIENT" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "They treat immune system disorders such as asthma, eczema, food allergies, insect sting allergies, and some autoimmune diseases.", "Allergists" },
                    { 2, "These doctors give you drugs to numb your pain or to put you under during surgery, childbirth, or other procedures. They monitor your vital signs while you’re under anesthesia.", "Anesthesiologists" },
                    { 3, "They’re experts on the heart and blood vessels. You might see them for heart failure, a heart attack, high blood pressure, or an irregular heartbeat.", "Cardiologists" },
                    { 4, "Have problems with your skin, hair, nails? Do you have moles, scars, acne, or skin allergies? Dermatologists can help.", "Dermatologists" },
                    { 5, "These are specialists in the nervous system, which includes the brain, spinal cord, and nerves. They treat strokes, brain and spinal tumors, epilepsy, Parkinson's disease, and Alzheimer's disease.", "Neurologists" },
                    { 6, "These lab doctors identify the causes of diseases by examining body tissues and fluids under microscopes.", "Pathologists" },
                    { 7, "These specialists in physical medicine and rehabilitation treat neck or back pain and sports or spinal cord injuries as well as other disabilities caused by accidents or diseases.", "Physiatrists" },
                    { 8, "They care for problems in your ankles and feet. That can include injuries from accidents or sports or from ongoing health conditions like diabetes.", "Podiatrists" },
                    { 9, "They use X-rays, ultrasound, and other imaging tests to diagnose diseases.", "Radiologists" },
                    { 10, "These are surgeons who care for men and women for problems in the urinary tract, like a leaky bladder.", "Urologists" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
