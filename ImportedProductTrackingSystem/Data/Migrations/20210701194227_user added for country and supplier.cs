using Microsoft.EntityFrameworkCore.Migrations;

namespace ImportedProductTrackingSystem.Data.Migrations
{
    public partial class useraddedforcountryandsupplier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IpmsUserId",
                table: "Suppliers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpmsUserId",
                table: "Countries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_IpmsUserId",
                table: "Suppliers",
                column: "IpmsUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_IpmsUserId",
                table: "Countries",
                column: "IpmsUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_AspNetUsers_IpmsUserId",
                table: "Countries",
                column: "IpmsUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_AspNetUsers_IpmsUserId",
                table: "Suppliers",
                column: "IpmsUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_AspNetUsers_IpmsUserId",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_AspNetUsers_IpmsUserId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_IpmsUserId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Countries_IpmsUserId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "IpmsUserId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "IpmsUserId",
                table: "Countries");
        }
    }
}
