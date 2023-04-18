using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class tablenamechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationRescheduling");

            migrationBuilder.CreateTable(
                name: "ReservationReschedulingRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    AccommodationReservationId = table.Column<int>(type: "INTEGER", nullable: false),
                    NewStartingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NewEndingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Achievable = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationReschedulingRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationReschedulingRequest_AccommodationReservation_AccommodationReservationId",
                        column: x => x.AccommodationReservationId,
                        principalTable: "AccommodationReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationReschedulingRequest_AccommodationReservationId",
                table: "ReservationReschedulingRequest",
                column: "AccommodationReservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationReschedulingRequest");

            migrationBuilder.CreateTable(
                name: "ReservationRescheduling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccommodationReservationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    NewEndingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NewStartingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationRescheduling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationRescheduling_AccommodationReservation_AccommodationReservationId",
                        column: x => x.AccommodationReservationId,
                        principalTable: "AccommodationReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRescheduling_AccommodationReservationId",
                table: "ReservationRescheduling",
                column: "AccommodationReservationId");
        }
    }
}
