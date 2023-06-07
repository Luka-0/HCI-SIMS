using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class forum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperGuide_User_guideID",
                table: "SuperGuide");

            migrationBuilder.AlterColumn<int>(
                name: "guideID",
                table: "SuperGuide",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ForumOwnerVs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GuestId = table.Column<int>(type: "INTEGER", nullable: false),
                    locationID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumOwnerVs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForumOwnerVs_Location_locationID",
                        column: x => x.locationID,
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ForumOwnerVs_User_GuestId",
                        column: x => x.GuestId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForumOwnerVs_GuestId",
                table: "ForumOwnerVs",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumOwnerVs_locationID",
                table: "ForumOwnerVs",
                column: "locationID");

            migrationBuilder.AddForeignKey(
                name: "FK_SuperGuide_User_guideID",
                table: "SuperGuide",
                column: "guideID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperGuide_User_guideID",
                table: "SuperGuide");

            migrationBuilder.DropTable(
                name: "ForumOwnerVs");

            migrationBuilder.AlterColumn<int>(
                name: "guideID",
                table: "SuperGuide",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_SuperGuide_User_guideID",
                table: "SuperGuide",
                column: "guideID",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
