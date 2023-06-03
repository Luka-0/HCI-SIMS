using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class added_notMapped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forum_User_UserId",
                table: "Forum");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumComment_User_UserId",
                table: "ForumComment");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ForumComment",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_ForumComment_UserId",
                table: "ForumComment",
                newName: "IX_ForumComment_UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Forum",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Forum_UserId",
                table: "Forum",
                newName: "IX_Forum_UserID");

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

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "ForumComment",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ForumComment_UserID",
                table: "ForumComment",
                newName: "IX_ForumComment_UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Forum",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Forum_UserID",
                table: "Forum",
                newName: "IX_Forum_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forum_User_UserId",
                table: "Forum",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumComment_User_UserId",
                table: "ForumComment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
