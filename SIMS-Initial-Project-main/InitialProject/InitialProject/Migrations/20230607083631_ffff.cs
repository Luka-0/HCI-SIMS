using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class ffff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VeryUseful",
                table: "CommentOwnerVs");

            migrationBuilder.AddColumn<bool>(
                name: "VeyUseful",
                table: "ForumOwnerVs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VeyUseful",
                table: "ForumOwnerVs");

            migrationBuilder.AddColumn<string>(
                name: "VeryUseful",
                table: "CommentOwnerVs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
