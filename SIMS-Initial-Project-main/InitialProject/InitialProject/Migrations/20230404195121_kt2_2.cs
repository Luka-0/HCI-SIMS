using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class kt2_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccommodationReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tidiness = table.Column<int>(type: "INTEGER", nullable: false),
                    Correctness = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    Images = table.Column<string>(type: "TEXT", nullable: false),
                    AccommodationReservationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccommodationReview_AccommodationReservation_AccommodationReservationId",
                        column: x => x.AccommodationReservationId,
                        principalTable: "AccommodationReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationRescheduling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    AccommodationReservationId = table.Column<int>(type: "INTEGER", nullable: false),
                    NewStartingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NewEndingDate = table.Column<DateTime>(type: "TEXT", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "TourReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GuideKnowledge = table.Column<int>(type: "INTEGER", nullable: false),
                    GuideLanguage = table.Column<int>(type: "INTEGER", nullable: false),
                    InterestLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Images = table.Column<string>(type: "TEXT", nullable: false),
                    TourReservationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourReview_TourReservation_TourReservationId",
                        column: x => x.TourReservationId,
                        principalTable: "TourReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationReview_AccommodationReservationId",
                table: "AccommodationReview",
                column: "AccommodationReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRescheduling_AccommodationReservationId",
                table: "ReservationRescheduling",
                column: "AccommodationReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_TourReview_TourReservationId",
                table: "TourReview",
                column: "TourReservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccommodationReview");

            migrationBuilder.DropTable(
                name: "ReservationRescheduling");

            migrationBuilder.DropTable(
                name: "TourReview");
        }
    }
}
