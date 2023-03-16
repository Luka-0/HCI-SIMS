using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class peta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tourReservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TourId = table.Column<int>(type: "INTEGER", nullable: false),
                    GuestNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    BookingGuestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tourReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tourReservations_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tourReservations_User_BookingGuestId",
                        column: x => x.BookingGuestId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tourReservations_BookingGuestId",
                table: "tourReservations",
                column: "BookingGuestId");

            migrationBuilder.CreateIndex(
                name: "IX_tourReservations_TourId",
                table: "tourReservations",
                column: "TourId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tourReservations");
        }
    }
}
