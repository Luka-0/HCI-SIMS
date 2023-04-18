using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class kt2_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tourReservations_Tour_TourId",
                table: "tourReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_tourReservations_User_BookingGuestId",
                table: "tourReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tourReservations",
                table: "tourReservations");

            migrationBuilder.RenameTable(
                name: "tourReservations",
                newName: "TourReservation");

            migrationBuilder.RenameIndex(
                name: "IX_tourReservations_TourId",
                table: "TourReservation",
                newName: "IX_TourReservation_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_tourReservations_BookingGuestId",
                table: "TourReservation",
                newName: "IX_TourReservation_BookingGuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourReservation",
                table: "TourReservation",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReceivedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ObtainedReason = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TourReservation_Tour_TourId",
                table: "TourReservation",
                column: "TourId",
                principalTable: "Tour",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourReservation_User_BookingGuestId",
                table: "TourReservation",
                column: "BookingGuestId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourReservation_Tour_TourId",
                table: "TourReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_TourReservation_User_BookingGuestId",
                table: "TourReservation");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourReservation",
                table: "TourReservation");

            migrationBuilder.RenameTable(
                name: "TourReservation",
                newName: "tourReservations");

            migrationBuilder.RenameIndex(
                name: "IX_TourReservation_TourId",
                table: "tourReservations",
                newName: "IX_tourReservations_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_TourReservation_BookingGuestId",
                table: "tourReservations",
                newName: "IX_tourReservations_BookingGuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tourReservations",
                table: "tourReservations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tourReservations_Tour_TourId",
                table: "tourReservations",
                column: "TourId",
                principalTable: "Tour",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tourReservations_User_BookingGuestId",
                table: "tourReservations",
                column: "BookingGuestId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
