using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class arrivalKeyPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TourKeyPointId",
                table: "TourReservation",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourReservation_TourKeyPointId",
                table: "TourReservation",
                column: "TourKeyPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourReservation_TourKeyPoint_TourKeyPointId",
                table: "TourReservation",
                column: "TourKeyPointId",
                principalTable: "TourKeyPoint",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourReservation_TourKeyPoint_TourKeyPointId",
                table: "TourReservation");

            migrationBuilder.DropIndex(
                name: "IX_TourReservation_TourKeyPointId",
                table: "TourReservation");

            migrationBuilder.DropColumn(
                name: "TourKeyPointId",
                table: "TourReservation");
        }
    }
}
