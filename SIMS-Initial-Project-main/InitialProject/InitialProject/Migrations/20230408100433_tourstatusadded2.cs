using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class tourstatusadded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Started",
                table: "Tour",
                newName: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Tour",
                newName: "Started");
        }
    }
}
