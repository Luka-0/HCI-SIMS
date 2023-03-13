using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class treca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccommodationId",
                table: "AccommodationReservation",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GuestId",
                table: "AccommodationReservation",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationReservation_AccommodationId",
                table: "AccommodationReservation",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationReservation_GuestId",
                table: "AccommodationReservation",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationReservation_Accommodation_AccommodationId",
                table: "AccommodationReservation",
                column: "AccommodationId",
                principalTable: "Accommodation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationReservation_User_GuestId",
                table: "AccommodationReservation",
                column: "GuestId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationReservation_Accommodation_AccommodationId",
                table: "AccommodationReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationReservation_User_GuestId",
                table: "AccommodationReservation");

            migrationBuilder.DropIndex(
                name: "IX_AccommodationReservation_AccommodationId",
                table: "AccommodationReservation");

            migrationBuilder.DropIndex(
                name: "IX_AccommodationReservation_GuestId",
                table: "AccommodationReservation");

            migrationBuilder.DropColumn(
                name: "AccommodationId",
                table: "AccommodationReservation");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "AccommodationReservation");
        }
    }
}
