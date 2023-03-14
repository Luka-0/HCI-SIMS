using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class dbupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contry",
                table: "Location",
                newName: "Country");

            migrationBuilder.AddColumn<int>(
                name: "locationID",
                table: "Accommodation",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accommodation_locationID",
                table: "Accommodation",
                column: "locationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodation_Location_locationID",
                table: "Accommodation",
                column: "locationID",
                principalTable: "Location",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodation_Location_locationID",
                table: "Accommodation");

            migrationBuilder.DropIndex(
                name: "IX_Accommodation_locationID",
                table: "Accommodation");

            migrationBuilder.DropColumn(
                name: "locationID",
                table: "Accommodation");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Location",
                newName: "Contry");
        }
    }
}
