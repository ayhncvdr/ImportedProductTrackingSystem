using Microsoft.EntityFrameworkCore.Migrations;

namespace ImportedProductTrackingSystem.Data.Migrations
{
    public partial class useradded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IpmsUserId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_IpmsUserId",
                table: "Products",
                column: "IpmsUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_IpmsUserId",
                table: "Products",
                column: "IpmsUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_IpmsUserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_IpmsUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IpmsUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");
        }
    }
}
