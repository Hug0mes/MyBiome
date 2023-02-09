 using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBiome.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductSize_ProductID",
                table: "ProductSize",
                column: "ProductID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSize_Products_ProductID",
                table: "ProductSize",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSize_Products_ProductID",
                table: "ProductSize");

            migrationBuilder.DropIndex(
                name: "IX_ProductSize_ProductID",
                table: "ProductSize");
        }
    }
}
