using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class tourReq2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "TourRequest",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TourRequest_LocationId",
                table: "TourRequest",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourRequest_Location_LocationId",
                table: "TourRequest",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourRequest_Location_LocationId",
                table: "TourRequest");

            migrationBuilder.DropIndex(
                name: "IX_TourRequest_LocationId",
                table: "TourRequest");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "TourRequest");
        }
    }
}
