using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class TourGuideProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userID",
                table: "Tour",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tour_userID",
                table: "Tour",
                column: "userID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_User_userID",
                table: "Tour",
                column: "userID",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tour_User_userID",
                table: "Tour");

            migrationBuilder.DropIndex(
                name: "IX_Tour_userID",
                table: "Tour");

            migrationBuilder.DropColumn(
                name: "userID",
                table: "Tour");
        }
    }
}
