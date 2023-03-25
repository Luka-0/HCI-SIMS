using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class praisejesus2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tour_Location_locationID",
                table: "Tour");

            migrationBuilder.DropForeignKey(
                name: "FK_TourKeyPoint_Tour_tourID",
                table: "TourKeyPoint");

            migrationBuilder.AlterColumn<int>(
                name: "tourID",
                table: "TourKeyPoint",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "locationID",
                table: "Tour",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_Location_locationID",
                table: "Tour",
                column: "locationID",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourKeyPoint_Tour_tourID",
                table: "TourKeyPoint",
                column: "tourID",
                principalTable: "Tour",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tour_Location_locationID",
                table: "Tour");

            migrationBuilder.DropForeignKey(
                name: "FK_TourKeyPoint_Tour_tourID",
                table: "TourKeyPoint");

            migrationBuilder.AlterColumn<int>(
                name: "tourID",
                table: "TourKeyPoint",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "locationID",
                table: "Tour",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_Location_locationID",
                table: "Tour",
                column: "locationID",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourKeyPoint_Tour_tourID",
                table: "TourKeyPoint",
                column: "tourID",
                principalTable: "Tour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
