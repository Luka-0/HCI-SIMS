using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class added_char_in_2_Forum_classes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isClosed",
                table: "Forum",
                newName: "IsClosed");

            migrationBuilder.AddColumn<char>(
                name: "Special",
                table: "ForumComment",
                type: "TEXT",
                nullable: false,
                defaultValue: ' ');

            migrationBuilder.AddColumn<char>(
                name: "Special",
                table: "Forum",
                type: "TEXT",
                nullable: false,
                defaultValue: ' ');
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Special",
                table: "ForumComment");

            migrationBuilder.DropColumn(
                name: "Special",
                table: "Forum");

            migrationBuilder.RenameColumn(
                name: "IsClosed",
                table: "Forum",
                newName: "isClosed");
        }
    }
}
