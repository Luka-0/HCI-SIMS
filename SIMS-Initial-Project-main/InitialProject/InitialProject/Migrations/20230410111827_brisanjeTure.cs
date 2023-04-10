using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class brisanjeTure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Tour_TourId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_TourKeyPoint_Tour_tourID",
                table: "TourKeyPoint");

            migrationBuilder.AddForeignKey(
                name: "TourId",
                table: "Image",
                column: "TourId",
                principalTable: "Tour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "TourID",
                table: "TourKeyPoint",
                column: "tourID",
                principalTable: "Tour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "TourId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "TourID",
                table: "TourKeyPoint");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Tour_TourId",
                table: "Image",
                column: "TourId",
                principalTable: "Tour",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourKeyPoint_Tour_tourID",
                table: "TourKeyPoint",
                column: "tourID",
                principalTable: "Tour",
                principalColumn: "Id");
        }
    }
}
