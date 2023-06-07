using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentOwnerVs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    forumId = table.Column<int>(type: "INTEGER", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    VeryUseful = table.Column<string>(type: "TEXT", nullable: false),
                    ByOwner = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentOwnerVs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentOwnerVs_ForumOwnerVs_forumId",
                        column: x => x.forumId,
                        principalTable: "ForumOwnerVs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentOwnerVs_forumId",
                table: "CommentOwnerVs",
                column: "forumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentOwnerVs");
        }
    }
}
