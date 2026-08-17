using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopera.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderCreateDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatDate",
                table: "Orders",
                newName: "CreateDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Orders",
                newName: "CreatDate");
        }
    }
}
