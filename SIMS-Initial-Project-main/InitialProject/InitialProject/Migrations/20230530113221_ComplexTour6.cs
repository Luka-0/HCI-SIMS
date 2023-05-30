using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class ComplexTour6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourRequest_ComplexTourRequest_ComplexTourRequestId",
                table: "TourRequest");

            migrationBuilder.AlterColumn<int>(
                name: "ComplexTourRequestId",
                table: "TourRequest",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

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

            migrationBuilder.AlterColumn<int>(
                name: "ComplexTourRequestId",
                table: "TourRequest",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TourRequest_ComplexTourRequest_ComplexTourRequestId",
                table: "TourRequest",
                column: "ComplexTourRequestId",
                principalTable: "ComplexTourRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
