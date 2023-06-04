using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class guideIdPropToVoucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperGuide_User_guideID",
                table: "SuperGuide");

            migrationBuilder.AddColumn<int>(
                name: "GuideId",
                table: "Voucher",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "guideID",
                table: "SuperGuide",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_GuideId",
                table: "Voucher",
                column: "GuideId");

            migrationBuilder.AddForeignKey(
                name: "FK_SuperGuide_User_guideID",
                table: "SuperGuide",
                column: "guideID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_User_GuideId",
                table: "Voucher",
                column: "GuideId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperGuide_User_guideID",
                table: "SuperGuide");

            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_User_GuideId",
                table: "Voucher");

            migrationBuilder.DropIndex(
                name: "IX_Voucher_GuideId",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "GuideId",
                table: "Voucher");

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
