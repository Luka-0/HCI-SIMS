using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class TourReservationVoucherProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VoucherId",
                table: "TourReservation",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourReservation_VoucherId",
                table: "TourReservation",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourReservation_Voucher_VoucherId",
                table: "TourReservation",
                column: "VoucherId",
                principalTable: "Voucher",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourReservation_Voucher_VoucherId",
                table: "TourReservation");

            migrationBuilder.DropIndex(
                name: "IX_TourReservation_VoucherId",
                table: "TourReservation");

            migrationBuilder.DropColumn(
                name: "VoucherId",
                table: "TourReservation");
        }
    }
}
