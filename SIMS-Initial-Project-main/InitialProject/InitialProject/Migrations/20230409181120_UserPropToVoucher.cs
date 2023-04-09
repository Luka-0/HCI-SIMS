using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class UserPropToVoucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Voucher",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_UserId",
                table: "Voucher",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_User_UserId",
                table: "Voucher",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_User_UserId",
                table: "Voucher");

            migrationBuilder.DropIndex(
                name: "IX_Voucher_UserId",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Voucher");
        }
    }
}
