using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class koznakoja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tourReservations_Tour_TourId",
                table: "tourReservations");

            migrationBuilder.AlterColumn<int>(
                name: "TourId",
                table: "tourReservations",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_tourReservations_Tour_TourId",
                table: "tourReservations",
                column: "TourId",
                principalTable: "Tour",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tourReservations_Tour_TourId",
                table: "tourReservations");

            migrationBuilder.AlterColumn<int>(
                name: "TourId",
                table: "tourReservations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tourReservations_Tour_TourId",
                table: "tourReservations",
                column: "TourId",
                principalTable: "Tour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
