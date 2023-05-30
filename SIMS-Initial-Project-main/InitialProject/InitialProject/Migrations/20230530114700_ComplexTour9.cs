using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class ComplexTour9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComplexTourRequestId",
                table: "TourRequest",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ComplexTourRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplexTourRequest", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourRequest_ComplexTourRequestId",
                table: "TourRequest",
                column: "ComplexTourRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourRequest_ComplexTourRequest_ComplexTourRequestId",
                table: "TourRequest",
                column: "ComplexTourRequestId",
                principalTable: "ComplexTourRequest",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourRequest_ComplexTourRequest_ComplexTourRequestId",
                table: "TourRequest");

            migrationBuilder.DropTable(
                name: "ComplexTourRequest");

            migrationBuilder.DropIndex(
                name: "IX_TourRequest_ComplexTourRequestId",
                table: "TourRequest");

            migrationBuilder.DropColumn(
                name: "ComplexTourRequestId",
                table: "TourRequest");
        }
    }
}
