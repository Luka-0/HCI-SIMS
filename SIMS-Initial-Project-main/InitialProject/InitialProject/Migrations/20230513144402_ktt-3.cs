using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class ktt3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TouristId",
                table: "TourRequest",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourRequest_TouristId",
                table: "TourRequest",
                column: "TouristId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourRequest_User_TouristId",
                table: "TourRequest",
                column: "TouristId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourRequest_User_TouristId",
                table: "TourRequest");

            migrationBuilder.DropIndex(
                name: "IX_TourRequest_TouristId",
                table: "TourRequest");

            migrationBuilder.DropColumn(
                name: "TouristId",
                table: "TourRequest");
        }
    }
}
