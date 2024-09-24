using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaReservationSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AuditoriumShowTimeOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditoriums_ShowTimes_ShowTimeId",
                table: "Auditoriums");

            migrationBuilder.DropIndex(
                name: "IX_Auditoriums_ShowTimeId",
                table: "Auditoriums");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Auditorium_TotalSeats",
                table: "Auditoriums");

            migrationBuilder.DropColumn(
                name: "ShowTimeId",
                table: "Auditoriums");

            migrationBuilder.AddColumn<int>(
                name: "AuditoriumId",
                table: "ShowTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShowTimes_AuditoriumId",
                table: "ShowTimes",
                column: "AuditoriumId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTimes_Auditoriums_AuditoriumId",
                table: "ShowTimes",
                column: "AuditoriumId",
                principalTable: "Auditoriums",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowTimes_Auditoriums_AuditoriumId",
                table: "ShowTimes");

            migrationBuilder.DropIndex(
                name: "IX_ShowTimes_AuditoriumId",
                table: "ShowTimes");

            migrationBuilder.DropColumn(
                name: "AuditoriumId",
                table: "ShowTimes");

            migrationBuilder.AddColumn<int>(
                name: "ShowTimeId",
                table: "Auditoriums",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auditoriums_ShowTimeId",
                table: "Auditoriums",
                column: "ShowTimeId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Auditorium_TotalSeats",
                table: "Auditoriums",
                sql: "TotalSeats <= 40");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditoriums_ShowTimes_ShowTimeId",
                table: "Auditoriums",
                column: "ShowTimeId",
                principalTable: "ShowTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
