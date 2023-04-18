using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class databaseupdatenewforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ownerId",
                table: "Accommodation",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accommodation_ownerId",
                table: "Accommodation",
                column: "ownerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodation_User_ownerId",
                table: "Accommodation",
                column: "ownerId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodation_User_ownerId",
                table: "Accommodation");

            migrationBuilder.DropIndex(
                name: "IX_Accommodation_ownerId",
                table: "Accommodation");

            migrationBuilder.DropColumn(
                name: "ownerId",
                table: "Accommodation");
        }
    }
}
