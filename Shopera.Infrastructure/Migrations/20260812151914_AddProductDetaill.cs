using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopera.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductDetaill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_ProductDetails_ProductDetailId",
                table: "ProductDetails");

            migrationBuilder.DropIndex(
                name: "IX_ProductDetails_ProductDetailId",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "ProductDetailId",
                table: "ProductDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductDetailId",
                table: "ProductDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductDetailId",
                table: "ProductDetails",
                column: "ProductDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_ProductDetails_ProductDetailId",
                table: "ProductDetails",
                column: "ProductDetailId",
                principalTable: "ProductDetails",
                principalColumn: "Id");
        }
    }
}
