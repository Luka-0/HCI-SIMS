using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class notnullallowedcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestReview_AccommodationReservation_ReservationId",
                table: "GuestReview");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationId",
                table: "GuestReview",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestReview_AccommodationReservation_ReservationId",
                table: "GuestReview",
                column: "ReservationId",
                principalTable: "AccommodationReservation",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestReview_AccommodationReservation_ReservationId",
                table: "GuestReview");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationId",
                table: "GuestReview",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GuestReview_AccommodationReservation_ReservationId",
                table: "GuestReview",
                column: "ReservationId",
                principalTable: "AccommodationReservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
