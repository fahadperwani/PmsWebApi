using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PmsWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AppointmentTableUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Appointments",
                newName: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Appointments",
                newName: "UserId");
        }
    }
}
