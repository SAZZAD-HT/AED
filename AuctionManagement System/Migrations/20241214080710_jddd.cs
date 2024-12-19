using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionManagement_System.Migrations
{
    /// <inheritdoc />
    public partial class jddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Length",
                table: "ProductTables",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "ProductTables");
        }
    }
}
