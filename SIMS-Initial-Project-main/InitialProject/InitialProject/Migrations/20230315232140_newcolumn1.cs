using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class newcolumn1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GuestId",
                table: "GuestGrade",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GuestGrade_GuestId",
                table: "GuestGrade",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestGrade_User_GuestId",
                table: "GuestGrade",
                column: "GuestId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestGrade_User_GuestId",
                table: "GuestGrade");

            migrationBuilder.DropIndex(
                name: "IX_GuestGrade_GuestId",
                table: "GuestGrade");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "GuestGrade");
        }
    }
}
