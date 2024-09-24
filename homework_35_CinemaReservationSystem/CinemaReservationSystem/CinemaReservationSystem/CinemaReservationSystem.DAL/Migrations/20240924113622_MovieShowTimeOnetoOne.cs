using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaReservationSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MovieShowTimeOnetoOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShowTimes_MovieId",
                table: "ShowTimes");

            migrationBuilder.CreateIndex(
                name: "IX_ShowTimes_MovieId",
                table: "ShowTimes",
                column: "MovieId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShowTimes_MovieId",
                table: "ShowTimes");

            migrationBuilder.CreateIndex(
                name: "IX_ShowTimes_MovieId",
                table: "ShowTimes",
                column: "MovieId");
        }
    }
}
