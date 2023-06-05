using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class trying_again : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "ForumComment",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Forum",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ForumComment_UserID",
                table: "ForumComment",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Forum_UserID",
                table: "Forum",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Forum_User_UserID",
                table: "Forum",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumComment_User_UserID",
                table: "ForumComment",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forum_User_UserID",
                table: "Forum");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumComment_User_UserID",
                table: "ForumComment");

            migrationBuilder.DropIndex(
                name: "IX_ForumComment_UserID",
                table: "ForumComment");

            migrationBuilder.DropIndex(
                name: "IX_Forum_UserID",
                table: "Forum");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "ForumComment");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Forum");
        }
    }
}
