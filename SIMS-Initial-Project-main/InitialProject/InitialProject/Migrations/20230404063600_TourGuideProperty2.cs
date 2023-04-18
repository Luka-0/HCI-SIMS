using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class TourGuideProperty2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tour_User_userID",
                table: "Tour");

            migrationBuilder.RenameColumn(
                name: "userID",
                table: "Tour",
                newName: "guideID");

            migrationBuilder.RenameIndex(
                name: "IX_Tour_userID",
                table: "Tour",
                newName: "IX_Tour_guideID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_User_guideID",
                table: "Tour",
                column: "guideID",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tour_User_guideID",
                table: "Tour");

            migrationBuilder.RenameColumn(
                name: "guideID",
                table: "Tour",
                newName: "userID");

            migrationBuilder.RenameIndex(
                name: "IX_Tour_guideID",
                table: "Tour",
                newName: "IX_Tour_userID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_User_userID",
                table: "Tour",
                column: "userID",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
