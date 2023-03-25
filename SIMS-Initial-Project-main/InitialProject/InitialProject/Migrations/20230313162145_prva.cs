using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class prva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Accommodation_AccommodationId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Tour_TourId",
                table: "Image");

            migrationBuilder.AlterColumn<int>(
                name: "TourId",
                table: "Image",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AccommodationId",
                table: "Image",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Accommodation_AccommodationId",
                table: "Image",
                column: "AccommodationId",
                principalTable: "Accommodation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Tour_TourId",
                table: "Image",
                column: "TourId",
                principalTable: "Tour",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Accommodation_AccommodationId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Tour_TourId",
                table: "Image");

            migrationBuilder.AlterColumn<int>(
                name: "TourId",
                table: "Image",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccommodationId",
                table: "Image",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Accommodation_AccommodationId",
                table: "Image",
                column: "AccommodationId",
                principalTable: "Accommodation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Tour_TourId",
                table: "Image",
                column: "TourId",
                principalTable: "Tour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
