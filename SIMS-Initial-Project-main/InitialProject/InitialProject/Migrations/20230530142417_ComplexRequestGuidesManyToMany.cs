using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class ComplexRequestGuidesManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComplexTourRequestUser",
                columns: table => new
                {
                    ComplexTourRequestsId = table.Column<int>(type: "INTEGER", nullable: false),
                    GuidesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplexTourRequestUser", x => new { x.ComplexTourRequestsId, x.GuidesId });
                    table.ForeignKey(
                        name: "FK_ComplexTourRequestUser_ComplexTourRequest_ComplexTourRequestsId",
                        column: x => x.ComplexTourRequestsId,
                        principalTable: "ComplexTourRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplexTourRequestUser_User_GuidesId",
                        column: x => x.GuidesId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplexTourRequestUser_GuidesId",
                table: "ComplexTourRequestUser",
                column: "GuidesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplexTourRequestUser");
        }
    }
}
