using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class newaccfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastRenovated",
                table: "Accommodation",
                newName: "LastRenovation");

            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                table: "Accommodation",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Accommodation");

            migrationBuilder.RenameColumn(
                name: "LastRenovation",
                table: "Accommodation",
                newName: "LastRenovated");
        }
    }
}
