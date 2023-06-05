using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class forumi1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "ForumComment",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ForumComment_LocationId",
                table: "ForumComment",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumComment_Location_LocationId",
                table: "ForumComment",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumComment_Location_LocationId",
                table: "ForumComment");

            migrationBuilder.DropIndex(
                name: "IX_ForumComment_LocationId",
                table: "ForumComment");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "ForumComment");
        }
    }
}
